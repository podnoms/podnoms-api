using System;

namespace PodNoms.Common.Data.ViewModels.Resources {
    public class SharingViewModel {
        public string Id { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }

    public class SharingResultViewModel : SharingViewModel {
        public string Url { get; set; }
    }

    public class SharingPublicViewModel {
        public string Title { get; set; }
        public string AudioUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}