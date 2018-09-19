using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PodNoms.Data.Models;
using PodNoms.Common;
using PodNoms.Common.Auth;
using Microsoft.EntityFrameworkCore;
using PodNoms.Common.Data.Settings;
using PodNoms.Common.Data.ViewModels.Resources;
using PodNoms.Common.Persistence;
using PodNoms.Common.Persistence.Repositories;
using PodNoms.Common.Services;
using PodNoms.Common.Services.Jobs;
using PodNoms.Common.Services.Processor;
using PodNoms.Common.Utils.RemoteParsers;

namespace PodNoms.Api.Controllers {
    [Route("[controller]")]
    [Authorize]
    public class EntryController : BaseAuthController {
        private readonly IPodcastRepository _podcastRepository;
        private readonly IEntryRepository _repository;

        private IConfiguration _options;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUrlProcessService _processor;
        private readonly AudioFileStorageSettings _audioFileStorageSettings;
        private readonly StorageSettings _storageSettings;

        public EntryController(IEntryRepository repository,
            IPodcastRepository podcastRepository,
            IUnitOfWork unitOfWork, IMapper mapper, IOptions<StorageSettings> storageSettings,
            IOptions<AudioFileStorageSettings> audioFileStorageSettings,
            IConfiguration options,
            IUrlProcessService processor,
            ILogger<EntryController> logger,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor) : base(contextAccessor, userManager, logger) {
            _podcastRepository = podcastRepository;
            _repository = repository;
            _options = options;
            _storageSettings = storageSettings.Value;
            _unitOfWork = unitOfWork;
            _audioFileStorageSettings = audioFileStorageSettings.Value;
            _mapper = mapper;
            _processor = processor;
        }

        private void _processEntry(PodcastEntry entry) {
            try {
                var extractJobId = BackgroundJob.Enqueue<IUrlProcessService>(
                    r => r.DownloadAudio(entry.Id));

                var uploadJobId = BackgroundJob.ContinueWith<IAudioUploadProcessService>(
                    extractJobId, r => r.UploadAudio(entry.Id, entry.AudioUrl));

                var cdnUrl = _options.GetSection("StorageSettings")["CdnUrl"];
                var imageContainer = _options.GetSection("ImageFileStorageSettings")["ContainerName"];

                BackgroundJob.ContinueWith<INotifyJobCompleteService>(
                    uploadJobId,
                    r => r.NotifyUser(entry.Podcast.AppUser.Id, "PodNoms",
                        $"{entry.Title} has finished processing",
                        entry.Podcast.GetThumbnailUrl(cdnUrl, imageContainer)
                    ));

                BackgroundJob.ContinueWith<INotifyJobCompleteService>(
                    uploadJobId,
                    r => r.SendCustomNotifications(entry.Podcast.Id, "PodNoms",
                        $"{entry.Title} has finished processing")
                );
            }
            catch (InvalidOperationException ex) {
                _logger.LogError($"Failed submitting job to processor\n{ex.Message}");
                entry.ProcessingStatus = ProcessingStatus.Failed;
            }
        }

        [HttpGet()]
        public async Task<ActionResult<List<PodcastEntryViewModel>>> GetAllForUser() {
            var entries = await _repository.GetAllForUserAsync(_applicationUser.Id);
            var results = _mapper.Map<List<PodcastEntry>, List<PodcastEntryViewModel>>(
                entries.OrderByDescending(e => e.CreateDate).ToList()
            );
            return Ok(results);
        }

        [HttpGet("all/{podcastSlug}")]
        public async Task<ActionResult<List<PodcastEntryViewModel>>> GetAllForSlug(string podcastSlug) {
            var entries = await _repository.GetAllForSlugAsync(podcastSlug);
            var results = _mapper.Map<List<PodcastEntry>, List<PodcastEntryViewModel>>(entries.ToList());

            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<PodcastEntryViewModel>> Post([FromBody] PodcastEntryViewModel item) {
            if (!ModelState.IsValid)
                return BadRequest("Invalid podcast entry posted");
            // first check url is valid
            var entry = _mapper.Map<PodcastEntryViewModel, PodcastEntry>(item);
            if (entry.ProcessingStatus == ProcessingStatus.Uploading ||
                entry.ProcessingStatus == ProcessingStatus.Processed) {
                // file was uploaded, just update repository and bail
                _repository.AddOrUpdate(entry);
                await _unitOfWork.CompleteAsync();
                var result = _mapper.Map<PodcastEntry, PodcastEntryViewModel>(entry);
                return Ok(result);
            }
            else {
                var status = await _processor.GetInformation(entry);
                if (status == AudioType.Valid) {
                    if (entry.ProcessingStatus != ProcessingStatus.Processing)
                        return BadRequest("Failed to create podcast entry");

                    if (string.IsNullOrEmpty(entry.ImageUrl)) {
                        entry.ImageUrl = $"{_storageSettings.CdnUrl}static/images/default-entry.png";
                    }

                    entry.Processed = false;
                    _repository.AddOrUpdate(entry);
                    try {
                        var succeeded = await _unitOfWork.CompleteAsync();
                        await _repository.LoadPodcastAsync(entry);
                        if (succeeded) {
                            _processEntry(entry);
                            var result = _mapper.Map<PodcastEntry, PodcastEntryViewModel>(entry);
                            return result;
                        }
                    }
                    catch (DbUpdateException e) {
                        _logger.LogError(e.Message);
                        return BadRequest(item);
                    }
                }
                else if ((status == AudioType.Playlist && YouTubeParser.ValidateUrl(item.SourceUrl))
                         || MixcloudParser.ValidateUrl(item.SourceUrl)) {
                    entry.ProcessingStatus = ProcessingStatus.Deferred;
                    var result = _mapper.Map<PodcastEntry, PodcastEntryViewModel>(entry);
                    return Accepted(result);
                }
            }

            return BadRequest("Failed to create podcast entry");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) {
            try {
                await _repository.DeleteAsync(new Guid(id));
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch (Exception ex) {
                _logger.LogError("Error deleting entry");
                _logger.LogError(ex.Message);
            }

            return BadRequest("Unable to delete entry");
        }

        [HttpPost("/preprocess")]
        public async Task<ActionResult<PodcastEntryViewModel>> PreProcess(PodcastEntryViewModel item) {
            var entry = await _repository.GetAsync(item.Id);
            entry.ProcessingStatus = ProcessingStatus.Accepted;
            var response = _processor.GetInformation(item.Id);
            entry.ProcessingStatus = ProcessingStatus.Processing;
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<PodcastEntry, PodcastEntryViewModel>(entry);
            return result;
        }

        [HttpPost("resubmit")]
        public async Task<IActionResult> ReSubmit([FromBody] PodcastEntryViewModel item) {
            var entry = await _repository.GetAsync(item.Id);
            entry.ProcessingStatus = ProcessingStatus.Processing;
            await _unitOfWork.CompleteAsync();
            if (entry.ProcessingStatus != ProcessingStatus.Processed) {
                _processEntry(entry);
            }

            return Ok(entry);
        }
    }
}