﻿using System.Collections.Generic;

namespace PodNoms.Common.Data.ViewModels.RssViewModels
{
    public class PodcastEnclosureViewModel {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public string PublishDate { get; set; }
        public string Language { get; set; }
        public string Copyright { get; set; }
        public string Owner { get; set; }
        public string OwnerEmail { get; set; }
        public string ShowUrl { get; set; }
        public List<PodcastEnclosureItemViewModel> Items { get; set; }
    }
}
