using System;
using System.Collections.Generic;

namespace PodNoms.Common.Data.ViewModels.Resources {
    public class TagViewModel {
        public string Id { get; set; }
        public string TagName { get; set; }
    }

    public class CategoryViewModel {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public List<SubcategoryViewModel> Children { get; set; }
    }

    public class SubcategoryViewModel {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
