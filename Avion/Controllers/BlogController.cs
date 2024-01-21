using Avion.Areas.Admin.ViewModels.Blog;
using Avion.Areas.Admin.ViewModels.BlogCategory;
using Avion.Areas.Admin.ViewModels.Category;
using Avion.Areas.Admin.ViewModels.Product;
using Avion.Areas.Admin.ViewModels.Tag;
using Avion.Helpers;
using Avion.Services;
using Avion.Services.Interfaces;
using Avion.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Controllers
{
    public class BlogController : Controller 
    {
        private readonly IBlogService _blogService;
        private readonly ITagService _tagService;
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogController(IBlogService blogService,
                              ITagService tagService,
                              IBlogCategoryService blogCategoryService)
        {
            _blogService = blogService;
            _tagService = tagService;
            _blogCategoryService = blogCategoryService;
        }

        public async Task<IActionResult> Index(int page=1, int take=4)
        {
            List<BlogVM> dbPaginatedDatas = await _blogService.GetPaginatedDatasAsync(page, take);
            List<TagVM> tags = await _tagService.GetAllAsync();
            List<BlogCategoryVM> blogCategories = await _blogCategoryService.GetAllAsync();

            int pageCount = await GetPageCountAsync(take);

            Paginate<BlogVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);

            BlogPageVM model = new()
            {
                PaginatedDatas = paginatedDatas,
                Tags = tags,
                BlogCategories = blogCategories
            };

            return View(model);
        }

        [HttpGet]
        private async Task<int> GetPageCountAsync(int take)
        {
            int blogCount = await _blogService.GetCountAsync();
            return (int)Math.Ceiling((decimal)(blogCount) / take);
        }


        [HttpGet]
        private async Task<int> GetPageCountByCategoryAsync(int id, int take)
        {
            int productCount = await _blogService.GetCountByCategoryAsync(id);

            return (int)Math.Ceiling((decimal)(productCount) / take);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            BlogVM blog = await _blogService.GetByIdAsync((int)id);
            if (blog is null) return NotFound();


            return View(blog);
        }


        [HttpGet]
        public async Task<IActionResult> GetBlogsByCategory(int? id, int page = 1, int take = 4)
        {
            if (id is null)
            {
                return BadRequest();
            }

            BlogCategoryVM existCategory = await _blogCategoryService.GetByIdAsync((int)id);

            if (existCategory == null)
            {
                return NotFound();
            }

            var count = await _blogService.GetCountByCategoryAsync((int) id);
            List<BlogVM> dbPaginatedDatasByCategory  = await _blogService.GetPaginatedDatasByCategoryAsync((int)(id), page, take);
            List<BlogCategoryVM> categories=await _blogCategoryService.GetAllAsync();
            List<TagVM> tags = await _tagService.GetAllAsync();


            int pageCount = await GetPageCountByCategoryAsync((int)id,take);
            Paginate<BlogVM> paginatedDatas = new(dbPaginatedDatasByCategory, page, pageCount);

            BlogPageVM model = new()
            {
                PaginatedDatas = paginatedDatas,
                BlogCategories = categories,
                Tags = tags,


            };

            return View(model);
        }

    

    }
}
