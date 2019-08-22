﻿using System;
using System.Net;
using System.Net.Http;
using EasyNetQ;
using EasyNetQ.Logging;
using Hangfire;
using Hangfire.Console;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PodNoms.Common.Auth;
using PodNoms.Common.Persistence;
using PodNoms.Common.Services.Jobs;
using PodNoms.Common.Services.Processor;
using PodNoms.Common.Services.Push.Extensions;
using PodNoms.Common.Services.Realtime;
using PodNoms.Common.Services.Startup;
using PodNoms.Common.Services.Waveforms;
using PodNoms.Data.Models;
using PodNoms.Jobs.Services;

namespace PodNoms.Jobs {
    public class JobsStartup {
        public static IConfiguration Configuration { get; private set; }
        public IHostEnvironment Env { get; }
        private WebProxy localDebuggingProxy = new WebProxy("http://localhost:9537");

        public JobsStartup(IHostEnvironment env, IConfiguration configuration) {
            Configuration = configuration;
            Env = env;
        }

        public void ConfigureServices(IServiceCollection services) {
            Console.WriteLine($"Configuring services");
            Console.WriteLine(
                $"JobSchedulerConnectionString: {Configuration.GetConnectionString("JobSchedulerConnection")}");
            Console.WriteLine($"RabbitMqConnection: {Configuration["RabbitMq:ExternalConnectionString"]}");
            Console.WriteLine($"ApiUrl: {Configuration["AppSettings:ApiUrl"]}");
            services.AddHangfire(options => {
                options.UseSqlServerStorage(Configuration.GetConnectionString("JobSchedulerConnection"));
                options.UseSimpleAssemblyNameTypeSerializer();
                options.UseRecommendedSerializerSettings();
                options.UseConsole();
                //TODO: unsure if this is needed - re-enable if we get DI issues
                // options.UseActivator (new HangfireActivator (serviceProvider));
            });

            services.AddHttpClient("podnoms", c => {
                c.BaseAddress = new Uri(Configuration["AppSettings:ApiUrl"]);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
                c.DefaultRequestHeaders.Add("User-Agent", "PodNoms-JobProcessor");
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler {
                Proxy = localDebuggingProxy,
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => {
                    return true;
                }
            });
            services
                .AddLogging()
                .AddPodNomsOptions(Configuration)
                .AddPodNomsMapping(Configuration)
                .AddSqlitePushSubscriptionStore(Configuration)
                .AddPushServicePushNotificationService()
                .AddDbContext<PodNomsDbContext>(options => {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                })
                .AddPodnomsSecurity(Configuration)
                .AddSharedDependencies()
                .AddSingleton<IBus>(RabbitHutch.CreateBus(Configuration["RabbitMq:ExternalConnectionString"]))
                .AddScoped<IWaveformGenerator, AWFWaveformGenerator>()
                .AddScoped<INotifyJobCompleteService, RabbitMqNotificationService>()
                .AddScoped<CachedAudioRetrievalService, CachedAudioRetrievalService>()
                .AddTransient<IRealTimeUpdater, SignalRClientUpdater>();

            LogProvider.SetCurrentLogProvider(ConsoleLogProvider.Instance);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHangfireServer();
            app.UseHangfireDashboard("/dashboard", new DashboardOptions {
                Authorization = new[] {new HangFireAuthorizationFilter()}
            });
            app.Run(async (context) => {
                await context.Response.WriteAsync("Hello World!");
            });
            JobBootstrapper.BootstrapJobs(false);
        }
    }
}
