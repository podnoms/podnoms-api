using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PodNoms.Common.Data.ViewModels.Resources;
using PodNoms.Common.Persistence.Repositories;
using PodNoms.Data.Models;

namespace PodNoms.Api.Controllers.Public {
    [Route("pub/podcast")]
    public class PublicPodcastController : Controller {
        private readonly IPodcastRepository _podcastRepository;
        private readonly IEntryRepository _entryRepository;
        private readonly IMapper _mapper;

        public PublicPodcastController(IPodcastRepository podcastRepository, IEntryRepository entryRepository,
            IMapper mapper) {
            _podcastRepository = podcastRepository;
            _entryRepository = entryRepository;
            _mapper = mapper;
        }

        [HttpGet("{user}/{podcast}")]
        public async Task<ActionResult<PodcastViewModel>> Get(string user, string podcast) {
            var result = await _podcastRepository.GetForUserAndSlugAsync(user, podcast);

            if (result == null) return NotFound();

            return _mapper.Map<Podcast, PodcastViewModel>(result);
        }

        [HttpGet("{userSlug}/{podcastSlug}/featured")]
        public async Task<ActionResult<PodcastEntryViewModel>> GetFeaturedEpisode(string userSlug, string podcastSlug) {
            var podcast = await _podcastRepository.GetAll()
                .Include(p => p.PodcastEntries)
                .SingleOrDefaultAsync(r => r.AppUser.Slug == userSlug && r.Slug == podcastSlug);

            var result = await _entryRepository.GetFeaturedEpisode(podcast);
            if (result == null) return NotFound();

            return _mapper.Map<PodcastEntry, PodcastEntryViewModel>(result);
        }

        [HttpGet("{podcastId}/featured")]
        public async Task<ActionResult<PodcastEntryViewModel>> GetFeaturedEpisode(string podcastId) {
            var podcast = await _podcastRepository.GetAsync(podcastId);
            if (podcast == null) return NotFound();

            var result = await _entryRepository.GetFeaturedEpisode(podcast);
            if (result == null) return NotFound();

            return _mapper.Map<PodcastEntry, PodcastEntryViewModel>(result);
        }
    }
}