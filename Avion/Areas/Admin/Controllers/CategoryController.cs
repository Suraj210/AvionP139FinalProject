using Avion.Areas.Admin.ViewModels.Brand;
using Avion.Areas.Admin.ViewModels.Category;
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
    public class CategoryController : MainController
    {
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly AppDbContext _context;

        public CategoryController(ICategoryService categoryService, IBrandService brandService,AppDbContext context)
        {
            _categoryService = categoryService;
            _brandService = brandService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int take = 8)
        {
            List<CategoryVM> dbPaginatedDatas = await _categoryService.GetPaginatedDatasWithIgnoreQuerryAsync(page, take);

            int pageCount = await GetPageCountAsync(take);

            Paginate<CategoryVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);

            return View(paginatedDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _categoryService.GetCountWithIgnoreFilterAsync();
            return (int)Math.Ceiling((decimal)(productCount) / take);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            CategoryVM category = await _categoryService.GetByIdIgnoreAsync((int)id);

            if (category is null) return NotFound();

            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete(int id)
        {
            CategoryVM category = await _categoryService.GetByIdIgnoreAsync(id);

            await _categoryService.SoftDeleteAsync(category);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var brands = _brandService.GetAllSelectedAsync();
            CategoryCreateVM viewModel = new CategoryCreateVM { 
               Brands  = brands
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CategoryCreateVM request)
        {


            if (!ModelState.IsValid)
            {
                request.Brands = _brandService.GetAllSelectedAsync();

                return View(request);
            }

            CategoryVM existCategory= await _categoryService.GetByNameWithoutTrackingAsync(request.Name);

            if (existCategory is not null)
            {
                request.Brands = _brandService.GetAllSelectedAsync();

                ModelState.AddModelError("Name", "This title already exists");

                return View(request);
            }


            await _categoryService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            await _categoryService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Category dbCategory = await _context.Categories.AsNoTracking()
                                              .IgnoreQueryFilters()
                                              .Where(m => m.Id == id)
                                              .Include(m => m.BrandCategories)
                                              .ThenInclude(m => m.Brand)
                                              .FirstOrDefaultAsync();

            if (dbCategory is null) return NotFound();

            var selectedBrands = dbCategory.BrandCategories.Select(m => m.BrandId).ToList();

            var brands = _context.Brands.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.Id.ToString(),
                Selected = selectedBrands.Contains(m.Id)
            }).ToList();




            return View(new CategoryEditVM()
            {
                Name = dbCategory.Name,
                Brands = brands,

            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, CategoryEditVM request)
        {
            if (id is null) return BadRequest();


            Category dbCategory = await _context.Categories.AsNoTracking()
                                               .IgnoreQueryFilters()
                                               .Where(m => m.Id == id)
                                               .Include(m => m.BrandCategories)
                                               .ThenInclude(m => m.Brand)
                                               .FirstOrDefaultAsync();

            if (dbCategory is null) return NotFound();


            var selectedBrands = dbCategory.BrandCategories.Select(m => m.BrandId).ToList();




            dbCategory.CreateTime = DateTime.Now;

            if (!ModelState.IsValid)
            {
                request.Brands = _brandService.GetAllSelectedAsync();
                return View(request);
            }

            CategoryVM existCategory = await _categoryService.GetByNameWithoutTrackingAsync(request.Name);


           


            if (existCategory is not null)
            {
                if (existCategory.Id == request.Id)
                {
                    await _categoryService.EditAsync(request);

                    return RedirectToAction(nameof(Index));
                }
                request.Brands = _brandService.GetAllSelectedAsync();
                ModelState.AddModelError("Name", "This name already exists");
                return View(request);
            }

            await _categoryService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }
    }
}
