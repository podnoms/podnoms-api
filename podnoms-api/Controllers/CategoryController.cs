using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using PodNoms.Data.Models;
using PodNoms.Common.Auth;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PodNoms.Common.Data.ViewModels.Resources;
using PodNoms.Common.Persistence.Repositories;

namespace PodNoms.Api.Controllers {
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class CategoryController : BaseAuthController {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager,
            ILogger<CategoryController> logger, ICategoryRepository categoryRepository, IMapper mapper)
            : base(contextAccessor, userManager, logger) {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryViewModel>>> Get() {
            var response = await _categoryRepository.GetAll()
                .Include(c => c.Subcategories)
                .OrderBy(r => r.Description)
                .ToListAsync();
            return Ok(_mapper.Map<List<Category>, List<CategoryViewModel>>(response));
        }
    }
}
