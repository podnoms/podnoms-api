using System;
using System.IO;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using PodNoms.Common.Persistence;
using PodNoms.Common.Persistence.Repositories;
using PodNoms.Common.Services.Realtime;
using PodNoms.Data.Enums;
using PodNoms.Common.Data.ViewModels;
using Microsoft.Extensions.Options;
using PodNoms.Common.Data.Settings;
using PodNoms.Common.Utils.Extensions;

namespace PodNoms.Common.Services.Processor {
    public class CachedAudioRetrievalService : RealtimeUpdatingProcessService {
        private readonly IEntryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public CachedAudioRetrievalService(IEntryRepository repository, IUnitOfWork unitOfWork,
            ILogger<AudioUploadProcessService> logger,
            IOptions<AppSettings> appSettings,
            IHttpClientFactory httpClientFactory,
            IRealTimeUpdater realtimeUpdater, IMapper mapper)
            : base(logger, realtimeUpdater, mapper) {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
            _httpClientFactory = httpClientFactory;
        }


        public async Task<string> RetrieveAudio(string authToken, Guid entryId, string url, string extension) {
            var entry = await _repository.GetAsync(entryId);
            await _sendProgressUpdate(
                authToken,
                entry.Id.ToString(),
                new ProcessingProgress(entry) {
                    ProcessingStatus = ProcessingStatus.Converting,
                    Progress = "Retrieving cached file"
                });
            string cacheFile = Path.Combine(
                    Path.GetTempPath(),
                    $"{System.Guid.NewGuid().ToString()}.{extension}"
                );
            var remoteUrl = $"{_appSettings.ApiUrl}/{url}";
            _logger.LogInformation($"Starting cache of {remoteUrl} to {cacheFile}");
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => {
                return true;
            };
            using (var client = new HttpClient(handler)) {
                var totalSize = await client.GetContentSizeAsync(remoteUrl);
                using (HttpResponseMessage response = client.GetAsync(remoteUrl, HttpCompletionOption.ResponseHeadersRead).Result) {
                    response.EnsureSuccessStatusCode();

                    using (Stream contentStream = await response.Content.ReadAsStreamAsync(),
                            fileStream = new FileStream(cacheFile, FileMode.Create,
                            FileAccess.Write, FileShare.None, 8192, true)) {
                        var totalRead = 0L;
                        var totalReads = 0L;
                        var buffer = new byte[8192];
                        var isMoreToRead = true;

                        do {
                            var read = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                            if (read == 0) {
                                isMoreToRead = false;
                            } else {
                                await fileStream.WriteAsync(buffer, 0, read);

                                totalRead += read;
                                totalReads += 1;

                                if (totalReads % 2000 == 0) {
                                    Console.WriteLine(string.Format("total bytes downloaded so far: {0:n0}", totalRead));
                                    var percentDone = Math.Round(((double)totalRead / (double)totalSize) * 100, 2);
                                    await _sendProgressUpdate(
                                        authToken,
                                        entry.Id.ToString(),
                                        new ProcessingProgress(entry) {
                                            ProcessingStatus = ProcessingStatus.Caching,
                                            Progress = "Retrieving cached file",
                                            Payload = new TransferProgress {
                                                Percentage = percentDone,
                                                TotalSize = totalSize.ToString()
                                            }
                                        });
                                    _logger.LogInformation($"{percentDone}% done.");
                                }
                            }
                        }
                        while (isMoreToRead);
                    }
                }
            }
            await _sendProgressUpdate(
                authToken,
                entry.Id.ToString(),
                new ProcessingProgress(entry) {
                    ProcessingStatus = ProcessingStatus.Processing,
                    Progress = "Retrieved cached file"
                });
            return cacheFile;
        }
    }
}