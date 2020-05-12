#nullable enable
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PodNoms.Common.Auth;
using PodNoms.Common.Data.Settings;
using PodNoms.Common.Persistence;
using PodNoms.Common.Persistence.Repositories;
using PodNoms.Common.Services.Jobs;
using PodNoms.Common.Services.Processor;
using PodNoms.Common.Utils.RemoteParsers;
using PodNoms.Data.Models;
using Tweetinvi;
using Tweetinvi.Events;
using Tweetinvi.Exceptions;
using Tweetinvi.Models;
using Tweetinvi.Streaming;

namespace PodNoms.Common.Services.Social {

    public class EpisodeFromTweetHandler : ITweetListener {
        private readonly TwitterStreamListenerSettings _twitterSettings;
        private readonly AppSettings _appSettings;
        private readonly ILogger<EpisodeFromTweetHandler> _logger;
        private readonly IServiceProvider _provider;
        private readonly IHttpClientFactory _clientFactory;

        private IFilteredStream? _stream;

        public EpisodeFromTweetHandler(
                    IOptions<TwitterStreamListenerSettings> twitterSettings,
                    IOptions<AppSettings> appSettings,
                    ILogger<EpisodeFromTweetHandler> logger,
                    IServiceProvider provider,
                    IHttpClientFactory clientFactory
                ) {
            _twitterSettings = twitterSettings.Value;
            _appSettings = appSettings.Value;
            _logger = logger;
            _provider = provider;
            _clientFactory = clientFactory;
        }

        public async Task<bool> StartAsync() {
            if (!__checkSettings()) {
                _logger.LogError("Twitter settings are incorrect or missing, stopping listener");
                return false;
            }

            await __startStreamInternal();

            return true;
        }
        public async Task StopAsync() {
            await __stopStreamInternal();
        }
        private bool __checkSettings() {
            return !string.IsNullOrEmpty(_twitterSettings.AccessToken) &&
                   !string.IsNullOrEmpty(_twitterSettings.AccessTokenSecret) &&
                   !string.IsNullOrEmpty(_twitterSettings.ApiKey) &&
                   !string.IsNullOrEmpty(_twitterSettings.ApiKeySecret);
        }
        private void __handleError(Exception e) {
            _logger.LogError($"Error consuming tweet: {e.Message}");
        }
        private async Task<bool> _sendReply(Tweetinvi.Models.ITweet tweet, string message) {
            var result = await TweetAsync.PublishTweetInReplyTo(message, tweet.Id);
            return result != null;
        }
        private async void __tryCreateEpisode(object? sender, MatchedTweetReceivedEventArgs incomingTweet) {

            _logger.LogDebug(incomingTweet.Json);

            ExceptionHandler.SwallowWebExceptions = false;
            ExceptionHandler.LogExceptions = true;
            try {
                if (incomingTweet.Tweet.InReplyToStatusId == null) {
                    return;
                }
                var tweetId = (long)incomingTweet.Tweet.InReplyToStatusId;
                var tweetToReplyTo = incomingTweet.Tweet;
                var tweetSource = incomingTweet.Tweet.CreatedBy.ScreenName;

                var user = await __getTargetUser(tweetSource);
                if (user == null) {
                    await _createPublicErrorResponse(
                        tweetToReplyTo,
                        $"Hi @{tweetSource}, sorry but I cannot find your account.\nPlease edit your profile and make sure your Twitter Handle is set correctly.\n{_appSettings.SiteUrl}/profile"
                    );
                    return;
                }

                using var scope = _provider.CreateScope();
                var (
                    processor,
                    podcastRepository,
                    entryRepository,
                    unitOfWork) = _getScopedServices(scope);

                var podcast = (await podcastRepository.GetRandomForUser(user.Id));
                if (podcast == null) {
                    await _createPublicErrorResponse(
                        tweetToReplyTo,
                        $"Hi @{tweetSource}, I cannot find the podcast to create this episode for, please make sure the podcast slug is the first word after @podnoms\n{_appSettings.SiteUrl}"
                    );
                    return;
                }

                var entry = new PodcastEntry {
                    Podcast = podcast,
                    Processed = false,
                    SourceUrl = incomingTweet.Tweet.Url
                };
                var status = await processor.GetInformation(entry);
                if (status != RemoteUrlType.SingleItem) {
                    await _createPublicErrorResponse(
                        tweetToReplyTo,
                        $"Hi @{tweetSource}, sorry but I cannot find any media to parse in this tweet.\n{_appSettings.SiteUrl}"
                    );
                    return;
                }

                entryRepository.AddOrUpdate(entry);
                await unitOfWork.CompleteAsync();
                var processId = BackgroundJob.Enqueue<ProcessNewEntryJob>(
                    e => e.ProcessEntry(entry.Id, string.Empty, null));

                var message = $"Hi @{tweetSource}, your request was processed succesfully, you can find your new episode in your podcatcher or here\n{podcast.GetPagesUrl(_appSettings.PagesUrl)}";
                BackgroundJob.ContinueJobWith<SendTweetJob>(
                    processId, (r) => r.SendTweet(tweetToReplyTo, message));

            } catch (Exception e) {
                _logger.LogError($"Error creating episode: {e.Message}");
            }
        }
        private (IUrlProcessService, IPodcastRepository, IEntryRepository, IUnitOfWork unitOfWork) _getScopedServices(IServiceScope scope) {
            var processService = scope.ServiceProvider.GetRequiredService<IUrlProcessService>();
            var podcastRepository = scope.ServiceProvider.GetRequiredService<IPodcastRepository>();
            var entryRepository = scope.ServiceProvider.GetRequiredService<IEntryRepository>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            return (
                processService,
                podcastRepository,
                entryRepository,
                unitOfWork
            );
        }
        private async Task<ApplicationUser> __getTargetUser(string twitterHandle) {
            using var scope = _provider.CreateScope();
            _logger.LogDebug($"Finding user");
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByTwitterHandleAsync(twitterHandle);
            return user;
        }

        private async Task<bool> __checkTweetHasMedia(string url) {
            try {
                var client = _clientFactory.CreateClient("podnoms");
                var request = new HttpRequestMessage(
                    System.Net.Http.HttpMethod.Get,
                    $"{_appSettings.ApiUrl}/urlprocess/_v?url={url}");
                var response = await client.SendAsync(request);
                return response.IsSuccessStatusCode;
            } catch (HttpRequestException) {
                _logger.LogError("Cannot contact the podnoms api");
            }
            return false;
        }
        private async Task _createPublicErrorResponse(ITweet tweetToReplyTo, string text) {
            _logger.LogError($"Error parsing incoming tweet.\n\t{text}");
            await _sendReply(
                tweetToReplyTo,
                text);
            return;
        }
        public async Task __startStreamInternal() {
            try {
                Tweetinvi.Auth.SetUserCredentials(
                    _twitterSettings.ApiKey,
                    _twitterSettings.ApiKeySecret,
                    _twitterSettings.AccessToken,
                    _twitterSettings.AccessTokenSecret);
                _stream = Stream.CreateFilteredStream();

                _logger.LogInformation("Starting Twitter stream");

                _stream.AddTrack("@podnoms");
                _stream.MatchingTweetReceived += __tryCreateEpisode;

                await _stream.StartStreamMatchingAnyConditionAsync();

            } catch (TwitterNullCredentialsException) {
                _logger.LogError("Twitter settings are incorrect or missing, stopping listener");
            } catch (Exception e) {
                _logger.LogError($"Unknown error starting stream: {e.Message}");
            }
        }
        public Task __stopStreamInternal() {
            _logger.LogInformation("Stopping Twitter stream");
            return Task.Factory.StartNew(() => {
                _stream?.StopStream();
            });
        }
    }
}
