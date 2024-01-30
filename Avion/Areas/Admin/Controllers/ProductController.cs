using Avion.Areas.Admin.ViewModels.Blog;
using Avion.Areas.Admin.ViewModels.Product;
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
    public class ProductController : MainController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public ProductController(IProductService productService, 
                                 ICategoryService categoryService,  
                                 IBrandService brandService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            List<ProductVM> dbPaginatedDatas = await _productService.GetPaginatedDatasWithIgnoreQuerryAsync(page, take);

            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);

            return View(paginatedDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _productService.GetCountWithIgnoreFilterAsync();
            return (int)Math.Ceiling((decimal)(productCount) / take);
        }



        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            ProductVM product = await _productService.GetByIdIgnoreAsync((int)id);

            if (product is null) return NotFound();

            
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete(int id)
        {
            ProductVM product = await _productService.GetByIdIgnoreAsync(id);

            await _productService.SoftDeleteAsync(product);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {

            ViewBag.productCategories = await GetCategoriesAsync();
            ViewBag.productBrands = await GetBrandsAsync();

            return View();
        }

        private async Task<SelectList> GetCategoriesAsync()
        {
            return new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
        }

        private async Task<SelectList> GetBrandsAsync()
        {
            return new SelectList(await _brandService.GetAllAsync(), "Id", "Name");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM request)
        {
            ViewBag.productCategories = await GetCategoriesAsync();
            ViewBag.productBrands = await GetBrandsAsync();

            if (!ModelState.IsValid)
            {
                return View(request);
            }


            ProductVM existData = await _productService.GetByNameWithoutTrackingAsync(request.Name);

            if (existData is not null)
            {

                ModelState.AddModelError("Name", "This product is already exists");

                return View(request);
            }


            foreach (var photo in request.Photos)
            {

                if (!photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photos", "File can be only in image format.");
                    return View(request);
                }

                if (!photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photos", "File size can be max 200kb.");
                    return View(request);
                }
            }

           await _productService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            await _productService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }
    }
}
