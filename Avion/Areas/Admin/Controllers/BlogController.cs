using Avion.Areas.Admin.ViewModels.Blog;
using Avion.Areas.Admin.ViewModels.BlogCategory;
using Avion.Data;
using Avion.Helpers;
using Avion.Helpers.Extentions;
using Avion.Models;
using Avion.Services;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Avion.Areas.Admin.Controllers
{
    public class BlogController : MainController
    {
        private readonly IBlogService _blogService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ITagService _tagService;
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogController(IBlogService blogService,
                                 AppDbContext context,
                                 IWebHostEnvironment env,
                                 ITagService tagService,
                                 IBlogCategoryService blogCategoryService)
        {
            _context = context;
            _env = env;
            _blogService = blogService;
            _tagService = tagService;
            _blogCategoryService = blogCategoryService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int take = 3)
        {
            List<BlogVM> dbPaginatedDatas = await _blogService.GetPaginatedDatasWithIgnoreQuerryAsync(page, take);

            int pageCount = await GetPageCountAsync(take);

            Paginate<BlogVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);

            return View(paginatedDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _blogService.GetCountWithIgnoreFilterAsync();
            return (int)Math.Ceiling((decimal)(productCount) / take);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            BlogVM blog = await _blogService.GetByIdIgnoreAsync((int)id);

            if (blog is null) return NotFound();

            return View(blog);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete(int id)
        {
            BlogVM blog = await _blogService.GetByIdIgnoreAsync(id);

            await _blogService.SoftDeleteAsync(blog);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var tags = _tagService.GetAllSelectedAsync();
            ViewBag.blogCategories = await GetCategoriesAsync();
            BlogCreateVM viewModel = new BlogCreateVM
            {
                Tags = tags
            };

            return View(viewModel);
        }

        private async Task<SelectList> GetCategoriesAsync()
        {
            return new SelectList(await _blogCategoryService.GetAllAsync(), "Id", "Name");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(BlogCreateVM request)
        {
            ViewBag.blogCategories = await GetCategoriesAsync();


            if (!ModelState.IsValid)
            {
                request.Tags = _tagService.GetAllSelectedAsync();

                return View(request);
            }

            BlogVM existBlog = await _blogService.GetByNameWithoutTrackingAsync(request.Title);

            if (existBlog is not null)
            {
                request.Tags = _tagService.GetAllSelectedAsync();

                ModelState.AddModelError("Title", "This title already exists");

                return View(request);
            }


            if (!request.Photo.CheckFileType("image/"))
            {
                request.Tags = _tagService.GetAllSelectedAsync();

                ModelState.AddModelError("Photo", "File can be only image format");
                return View(request);
            }

            if (!request.Photo.CheckFileSize(500))
            {
                request.Tags = _tagService.GetAllSelectedAsync();

                ModelState.AddModelError("Photo", "File size can be max 500 kb");
                return View(request);
            }



            await _blogService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            await _blogService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            ViewBag.blogCategories = await GetCategoriesAsync();
            Blog dbBlog = await _context.Blogs.AsNoTracking()
                                              .IgnoreQueryFilters()
                                              .Where(m => m.Id == id)
                                              .Include(bc => bc.BlogCategory)
                                              .Include(m => m.BlogTags)
                                              .ThenInclude(m => m.Tag)
                                              .FirstOrDefaultAsync();

            if (dbBlog is null) return NotFound();

            var selectedTags = dbBlog.BlogTags.Select(m => m.TagId).ToList();

            var tags = _context.Tags.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.Id.ToString(),
                Selected = selectedTags.Contains(m.Id)
            }).ToList();




            return View(new BlogEditVM()
            {
                Title = dbBlog.Title,
                Description = dbBlog.Description,
                Image = dbBlog.Image,
                Tags = tags,
                BlogCategoryId = dbBlog.BlogCategoryId

            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, BlogEditVM request)
        {
            if (id is null) return BadRequest();

            ViewBag.blogCategories = await GetCategoriesAsync();

            Blog dbBlog = await _context.Blogs.IgnoreQueryFilters()
                                              .Where(m => m.Id == id)
                                              .Include(bc => bc.BlogCategory)
                                              .Include(m => m.BlogTags)
                                              .ThenInclude(m => m.Tag)
                                              .FirstOrDefaultAsync();

            if (dbBlog is null) return NotFound();


            var selectedTags = dbBlog.BlogTags.Select(m => m.TagId).ToList();




            request.Image = dbBlog.Image;
            dbBlog.BlogCategoryId = request.BlogCategoryId;


            if (!ModelState.IsValid)
            {
                request.Tags = _tagService.GetAllSelectedAsync();
                return View(request);
            }

            BlogVM existBlog = await _blogService.GetByNameWithoutTrackingAsync(request.Title);


            if (request.Photo != null)
            {

                if (!request.Photo.CheckFileType("image/"))
                {
                    request.Tags = _tagService.GetAllSelectedAsync();
                    ModelState.AddModelError("Photos", "File can only be in image format");
                    return View(request);

                }

                if (!request.Photo.CheckFileSize(500))
                {
                    request.Tags = _tagService.GetAllSelectedAsync();
                    ModelState.AddModelError("Photos", "File size can be max 500 kb");
                    return View(request);
                }



            }


            if (existBlog is not null)
            {
                if (existBlog.Id == request.Id)
                {
                    await _blogService.EditAsync(request);

                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("Name", "This name already exists");
                return View(request);
            }

            await _blogService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }

    }
}
