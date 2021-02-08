﻿using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PodNoms.Common.Data.Messages;
using PodNoms.Common.Services.Hubs;
using PodNoms.Common.Services.Jobs;
using PodNoms.Data.Models;

namespace PodNoms.Common.Services.Hosted {
    public class RabbitMQService : BackgroundService {
        private readonly IBus _bus;
        private readonly HubLifetimeManager<AudioProcessingHub> _hub;
        private readonly ILogger<RabbitMQService> _logger;
        private readonly AutoSubscriber _subscriber;

        public RabbitMQService(IBus bus,
            HubLifetimeManager<AudioProcessingHub> hub,
            IServiceScopeFactory serviceScopeFactory, ILogger<RabbitMQService> logger) {
            _bus = bus;
            _hub = hub;
            _logger = logger;

            _subscriber = new AutoSubscriber(_bus, "_applications_subscriptionId_prefix");
            _subscriber.Subscribe(new[] {Assembly.GetExecutingAssembly()});
            try {
                _bus.PubSub.Subscribe<RealtimeUpdateMessage>(
                    "podnoms_message_realtimeupdate",
                    message => {
                        _logger.LogDebug($"(RabbitMQService) Consuming: {message.Data}");
                        _hub.SendUserAsync(
                            message.UserId,
                            message.ChannelName, //userId, 
                            new[] {message.Data});
                    });
                _bus.PubSub.Subscribe<NotifyUserMessage>(
                    "podnoms_message_notifyuser",
                    message => {
                        _logger.LogDebug($"(RabbitMQService) Consuming: {message.Body}");
                        using var scope = serviceScopeFactory.CreateScope();
                        var service =
                            scope.ServiceProvider.GetRequiredService<INotifyJobCompleteService>();
                        service.NotifyUser(
                            message.UserId,
                            message.Title,
                            message.Body,
                            message.Target,
                            message.Image, NotificationOptions.UploadCompleted);
                    }
                );
                _bus.PubSub.Subscribe<CustomNotificationMessage>(
                    "podnoms_message_customnotification",
                    message => {
                        _logger.LogDebug($"(RabbitMQService) Consuming: {message.Body}");
                        using (var scope = serviceScopeFactory.CreateScope()) {
                            var service =
                                scope.ServiceProvider.GetRequiredService<INotifyJobCompleteService>();
                            service.SendCustomNotifications(
                                message.PodcastId,
                                "YOU NEED TO CHANGE THIS",
                                "PodNoms",
                                $"{message.Title} has finished processing",
                                message.Url);
                        }
                    }
                );
            } catch (Exception e) {
                _logger.LogError("Unable to start realtime queue listeners");
                _logger.LogError(e.Message);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            stoppingToken.Register(() => Console.WriteLine("RabbitMQService is stopping."));
            while (!stoppingToken.IsCancellationRequested) {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _bus.Dispose();
        }
    }
}
