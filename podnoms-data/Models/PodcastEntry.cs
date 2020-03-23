﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PodNoms.Data.Annotations;
using PodNoms.Data.Enums;
using PodNoms.Data.Interfaces;

namespace PodNoms.Data.Models {
    public enum ShareOptions {
        Public = (1 << 0),
        Private = (1 << 1),
        Download = (1 << 2)
    }
    public class PodcastEntry : BaseEntity, ISluggedEntity {

        [SlugField(sourceField: "Title")] public string Slug { get; set; }

        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SourceUrl { get; set; }
        public string AudioUrl { get; set; }
        public float AudioLength { get; set; }
        public long AudioFileSize { get; set; }
        public string ImageUrl { get; set; }
        public string ProcessingPayload { get; set; }
        public ProcessingStatus ProcessingStatus { get; set; } = ProcessingStatus.Accepted;
        public DateTime? SourceCreateDate { get; set; }
        public string SourceItemId { get; set; }

        public int MetadataStatus { get; set; } = 0;

        public int ShareOptions { get; set; }
        public bool Processed { get; set; }
        public bool WaveformGenerated { get; set; }
        public Guid PodcastId { get; set; }

        [JsonIgnore]
        public Podcast Podcast { get; set; }

        [JsonIgnore]
        public List<PodcastEntrySharingLink> SharingLinks { get; set; }

        [JsonIgnore]
        public List<ActivityLogPodcastEntry> ActivityLogs { get; set; }

        private string extension => "jpg";

        public string GetDownloadUrl(string downloadUrlRoot) => $"{downloadUrlRoot}/{this.Id}";
        public string GetPcmUrl(string cdnUrl, string containerName) => $"{cdnUrl}/{containerName}/{Id}.json";
        public string GetAudioUrl(string audioUrl) => GetAudioUrl(audioUrl, "mp3");
        public string GetAudioUrl(string audioUrl, string extension) => $"{audioUrl}/{Id}.{extension}";
        public string GetRssAudioUrl(string audioUrl) => GetAudioUrl(audioUrl, "mp3");
        public string GetRawAudioUrl(string cdnUrl, string containerName, string extension) =>
            Flurl.Url.Combine(cdnUrl, containerName, $"{Id}.{extension}");

        public string GetImageUrl(string cdnUrl, string containerName) => ImageUrl.StartsWith("http") ?
                ImageUrl :
                Flurl.Url.Combine(cdnUrl, containerName,
                $"entry/{Id}.{extension}?width=725&height=748&cb={System.Guid.NewGuid()}");

        public string GetThumbnailUrl(string cdnUrl, string containerName) => ImageUrl.StartsWith("http") ?
                ImageUrl :
                Flurl.Url.Combine(cdnUrl, containerName,
                $"entry/{Id}.{extension}?width=64&height=64&cb={System.Guid.NewGuid()}");

        public string GetInternalStorageUrl(string cdnUrl) => $"{cdnUrl}/{AudioUrl}";

        public string GetFileDownloadName() => $"{Title}.mp3";

        public string GetPagesUrl(string pagesUrl) =>
            Flurl.Url.Combine(pagesUrl, this.Podcast.AppUser.Slug, this.Podcast.Slug, this.Slug);

        public string GetCacheKey(string type = "rss") =>
            $"podcast|{type}|{this.Podcast.AppUser.Slug}|{this.Podcast.Slug}";
    }
}
