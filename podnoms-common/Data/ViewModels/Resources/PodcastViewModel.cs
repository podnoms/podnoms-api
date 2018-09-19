using System;
using System.Collections.Generic;

namespace PodNoms.Common.Data.ViewModels.Resources {
    public class PodcastViewModel {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string CustomDomain { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string RssUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public List<PodcastEntryViewModel> PodcastEntries { get; set; }
        public CategoryViewModel Category { get; set; }
        public List<SubcategoryViewModel> Subcategories { get; set; }
        public List<NotificationViewModel> Notifications { get; set; }
    }
}