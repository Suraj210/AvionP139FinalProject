using Avion.Areas.Admin.ViewModels.Blog;
using Avion.Areas.Admin.ViewModels.Brand;
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
    public class BrandController : MainController
    {
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;


        public BrandController(IBrandService brandService, 
                               ICategoryService categoryService,
                               AppDbContext context)
        {
            _brandService = brandService;
            _categoryService = categoryService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int take = 8)
        {
            List<BrandVM> dbPaginatedDatas = await _brandService.GetPaginatedDatasWithIgnoreQuerryAsync(page, take);

            int pageCount = await GetPageCountAsync(take);

            Paginate<BrandVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);

            return View(paginatedDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _brandService.GetCountWithIgnoreFilterAsync();
            return (int)Math.Ceiling((decimal)(productCount) / take);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            BrandVM brand = await _brandService.GetByIdIgnoreAsync((int)id);

            if (brand is null) return NotFound();

            return View(brand);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete(int id)
        {
            BrandVM brand = await _brandService.GetByIdIgnoreAsync(id);

            await _brandService.SoftDeleteAsync(brand);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var categories = _categoryService.GetAllSelectedAsync();
            BrandCreateVM viewModel = new BrandCreateVM
            {
                Categories = categories
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(BrandCreateVM request)
        {


            if (!ModelState.IsValid)
            {
                request.Categories = _categoryService.GetAllSelectedAsync();

                return View(request);
            }

            BrandVM existBlog = await _brandService.GetByNameWithoutTrackingAsync(request.Name);

            if (existBlog is not null)
            {
                request.Categories = _categoryService.GetAllSelectedAsync();

                ModelState.AddModelError("Title", "This title already exists");

                return View(request);
            }


            if (!request.Photo.CheckFileType("image/"))
            {
                request.Categories = _categoryService.GetAllSelectedAsync();

                ModelState.AddModelError("Photo", "File can be only image format");
                return View(request);
            }

            if (!request.Photo.CheckFileSize(500))
            {
                request.Categories = _categoryService.GetAllSelectedAsync();

                ModelState.AddModelError("Photo", "File size can be max 500 kb");
                return View(request);
            }



            await _brandService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            await _brandService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }


        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Brand dbBrand = await _context.Brands.AsNoTracking()
                                              .IgnoreQueryFilters()
                                              .Where(m => m.Id == id)
                                              .Include(m => m.BrandCategories)
                                              .ThenInclude(m => m.Category)
                                              .FirstOrDefaultAsync();

            if (dbBrand is null) return NotFound();

            var selectedCategories = dbBrand.BrandCategories.Select(m => m.CategoryId).ToList();

            var categories = _context.Categories.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.Id.ToString(),
                Selected = selectedCategories.Contains(m.Id)
            }).ToList();




            return View(new BrandEditVM()
            {
                Name = dbBrand.Name,
                Image = dbBrand.Image,
                Categories = categories,

            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, BrandEditVM request)
        {
            if (id is null) return BadRequest();


            Brand dbBrand = await _context.Brands.AsNoTracking()
                                              .IgnoreQueryFilters()
                                              .Where(m => m.Id == id)
                                              .Include(m => m.BrandCategories)
                                              .ThenInclude(m => m.Category)
                                              .FirstOrDefaultAsync();

            if (dbBrand is null) return NotFound();


            var selectedCategories = dbBrand.BrandCategories.Select(m => m.CategoryId).ToList();




            request.Image = dbBrand.Image;
            dbBrand.CreateTime = DateTime.Now;

            if (!ModelState.IsValid)
            {
                request.Categories = _categoryService.GetAllSelectedAsync();
                return View(request);
            }

            BrandVM existBrand = await _brandService.GetByNameWithoutTrackingAsync(request.Name);


            if (request.Photo != null)
            {

                if (!request.Photo.CheckFileType("image/"))
                {
                    request.Categories = _categoryService.GetAllSelectedAsync();
                    ModelState.AddModelError("Photos", "File can only be in image format");
                    return View(request);

                }

                if (!request.Photo.CheckFileSize(500))
                {
                    request.Categories = _categoryService.GetAllSelectedAsync();
                    ModelState.AddModelError("Photos", "File size can be max 500 kb");
                    return View(request);
                }



            }


            if (existBrand is not null)
            {
                if (existBrand.Id == request.Id)
                {
                    await _brandService.EditAsync(request);

                    return RedirectToAction(nameof(Index));
                }
                request.Categories = _categoryService.GetAllSelectedAsync();
                ModelState.AddModelError("Name", "This name already exists");
                return View(request);
            }

            await _brandService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }
    }
}
