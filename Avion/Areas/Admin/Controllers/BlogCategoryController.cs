using Avion.Areas.Admin.ViewModels.BlogCategory;
using Avion.Areas.Admin.ViewModels.Tag;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Areas.Admin.Controllers
{
    public class BlogCategoryController : MainController
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogCategoryController(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _blogCategoryService.GetAllIgnoreAdminAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(BlogCategoryCreateVM request)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }

            BlogCategoryVM existBlogCategory = await _blogCategoryService.GetByNameWithoutTrackingAsync(request.Name);

            if (existBlogCategory != null)
            {
                ModelState.AddModelError("Name", "This Category is already exists");
                return View();
            }


            await _blogCategoryService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogCategoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete(int id)
        {
            BlogCategoryVM blogCategory = await _blogCategoryService.GetByIdIgnoreAsync(id);

            await _blogCategoryService.SoftDeleteAsync(blogCategory);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            BlogCategoryVM dbBlogCategory = await _blogCategoryService.GetByIdWithoutTrackingAsync((int)id);

            if (dbBlogCategory is null) return NotFound();

            return View(new BlogCategoryEditVM
            {
                Name = dbBlogCategory.Name
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, BlogCategoryEditVM request)
        {
            if (id is null) return BadRequest();

            BlogCategoryVM dbBlogCategory = await _blogCategoryService.GetByIdWithoutTrackingAsync((int)id);

            if (dbBlogCategory is null) return NotFound();


            if (!ModelState.IsValid)
            {
                return View();
            }

            BlogCategoryVM existBlogCategory = await _blogCategoryService.GetByNameWithoutTrackingAsync(request.Name);



            if (existBlogCategory != null)
            {
                if (existBlogCategory.Id == request.Id)
                {
                    await _blogCategoryService.EditAsync(request);

                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Name", "This Category is already exists");
                return View();
            }

            await _blogCategoryService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }
    }
}
