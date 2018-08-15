﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PodNoms.Api.Models;
using PodNoms.Api.Models.ViewModels;
using PodNoms.Api.Persistence;

namespace PodNoms.Api.Providers {
    internal class PodcastCategoryResolver : IMemberValueResolver<PodcastViewModel, Podcast, string, Category> {
        private readonly ICategoryRepository _categoryRepository;
        public PodcastCategoryResolver(ICategoryRepository categoryRespository) {
            this._categoryRepository = categoryRespository;
        }

        public Category Resolve(PodcastViewModel source, Podcast destination, string sourceMember, Category destMember, ResolutionContext context) {
            var category = _categoryRepository.GetAll()
                .Where(r => r.Id.ToString() == sourceMember)
                .FirstOrDefault();
            return category;
        }
    }
    internal class PodcastSubcategoryResolver : IMemberValueResolver<PodcastViewModel, Podcast, ICollection<SubcategoryViewModel>, ICollection<Subcategory>> {
        private readonly ICategoryRepository _categoryRepository;
        public PodcastSubcategoryResolver(ICategoryRepository categoryRespository) {
            this._categoryRepository = categoryRespository;
        }
        public ICollection<Subcategory> Resolve(PodcastViewModel source, Podcast destination, ICollection<SubcategoryViewModel> sourceMember, ICollection<Subcategory> destMember, ResolutionContext context) {
            var results = _categoryRepository.GetAllSubcategories()
                .Where(r => sourceMember.Select(e => e.Id.ToString()).Contains(r.Id.ToString()))
                .ToList();
            return results;
        }
    }
}