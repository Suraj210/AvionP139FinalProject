using Avion.Areas.Admin.ViewModels.Brand;
using Avion.Areas.Admin.ViewModels.Category;
using Avion.Areas.Admin.ViewModels.Product;
using Avion.Helpers;
using Avion.Models;
using Avion.Services.Interfaces;
using Avion.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Avion.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        public ShopController(IProductService productService,
                              ICategoryService categoryService,
                              IBrandService brandService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int take = 12)
        {
            List<ProductVM> dbPaginatedDatas = await _productService.GetPaginatedDatasAsync(page, take);

            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);
            List<CategoryVM> categories = await _categoryService.GetAllAsync();
            List<BrandVM> brands = await _brandService.GetAllAsync();

            ShopPageVM model = new()
            {
                PaginatedDatas = paginatedDatas,
                Categories = categories,
                Brands = brands
            };

            return View(model);
        }


        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _productService.GetProductCountAsync();
            return (int)Math.Ceiling((decimal)(productCount) / take);
        }

        [HttpGet]

        public async Task<IActionResult> ProductDetail(int? id)
        {
            if (id is null) return BadRequest();

            ProductVM product = await _productService.GetByIdAsync((int)id);

            if (product is null) return NotFound();

            List<ProductVM> featuredProducts = await _productService.GetAllByTakeAsync(6);

            ProductDetailVM model = new()
            {
                Product = product,
                FeaturedProducts = featuredProducts
            };

            return View(model);
        }

        //Filter by Category Name Methods

        [HttpGet]
        private async Task<int> GetPageCountByCategoryAsync(int id, int take)
        {
            int productCount = await _productService.GetCountByCategoryAsync(id);

            return (int)Math.Ceiling((decimal)(productCount) / take);
        }


        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(int? id, int page = 1, int take = 4)
        {
            if (id is null)
            {
                return BadRequest();
            }

            CategoryVM existCategory = await _categoryService.GetByIdAsync((int)id);

            if (existCategory == null)
            {
                return NotFound();
            }

            var count = await _productService.GetCountByCategoryAsync((int)id);
            List<ProductVM> dbPaginatedDatasByCategory = await _productService.GetPaginatedDatasByCategoryAsync((int)(id), page, take);
            List<CategoryVM> categories = await _categoryService.GetAllAsync();
            List<BrandVM> brands = await _brandService.GetAllAsync();



            int pageCount = await GetPageCountByCategoryAsync((int)id, take);
            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatasByCategory, page, pageCount);

            ShopPageVM model = new()
            {
                PaginatedDatas = paginatedDatas,
                Categories = categories,
                Brands = brands


            };

            return View(model);
        }


        //Filter by Brand Name Methods

        [HttpGet]

        private async Task<int> GetPageCountByBrandAsync(int id, int take)
        {
            int productCount = await _productService.GetCountByBrandAsync(id);

            return (int)Math.Ceiling((decimal)(productCount) / take);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByBrand(int? id, int page = 1, int take = 4)
        {
            if (id is null)
            {
                return BadRequest();
            }

            BrandVM existBrand = await _brandService.GetByIdAsync((int)id);

            if (existBrand == null)
            {
                return NotFound();
            }

            var count = await _productService.GetCountByBrandAsync((int)id);
            List<ProductVM> dbPaginatedDatasByBrand = await _productService.GetPaginatedDatasByBrandAsync((int)(id), page, take);
            List<CategoryVM> categories = await _categoryService.GetAllAsync();
            List<BrandVM> brands = await _brandService.GetAllAsync();



            int pageCount = await GetPageCountByBrandAsync((int)id, take);
            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatasByBrand, page, pageCount);

            ShopPageVM model = new()
            {
                PaginatedDatas = paginatedDatas,
                Categories = categories,
                Brands = brands


            };

            return View(model);
        }

        //Search Methods

        public async Task<IActionResult> Search(string searchText, int page = 1, int take = 6)
        {

            if (searchText == null)
            {
                return RedirectToAction("Index", "Shop");
            }

            var count = await _productService.GetCountBySearch(searchText);
            List<ProductVM> dbPaginatedDatasByBrand = await _productService.SearchAsync(searchText, page, take);
            List<CategoryVM> categories = await _categoryService.GetAllAsync();
            List<BrandVM> brands = await _brandService.GetAllAsync();



            int pageCount = await GetPageCountBySearchAsync(searchText, take);
            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatasByBrand, page, pageCount);

            ShopPageVM model = new()
            {
                PaginatedDatas = paginatedDatas,
                Categories = categories,
                Brands = brands,
                SearchText = searchText
            };

            return View(model);


        }


        private async Task<int> GetPageCountBySearchAsync(string searchText, int take)
        {
            int productCount = await _productService.GetCountBySearch(searchText);

            return (int)Math.Ceiling((decimal)(productCount) / take);
        }


        //Sort Methos

        public async Task<IActionResult> Sort(string sortValue, int page = 1, int take = 12)
        {
            List<ProductVM> products = new();

            if (sortValue == "1")
            {
                products = await _productService.OrderByDate(page, take);
            };
            if (sortValue == "2")
            {
                products = await _productService.OrderByNameAsc(page, take);

            };
            if (sortValue == "3")
            {
                products = await _productService.OrderByNameDesc(page, take);

            };
            if (sortValue == "5")
            {
                products = await _productService.OrderByPriceAsc(page, take);

            };

            if (sortValue == "4")
            {
                products = await _productService.OrderByPriceDesc(page, take);

            };


            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductVM> paginatedDatas = new(products, page, pageCount);

            List<CategoryVM> categories = await _categoryService.GetAllAsync();
            List<BrandVM> brands = await _brandService.GetAllAsync();


            int count = await _productService.GetProductCountAsync();

            ShopPageVM model = new()
            {
                PaginatedDatas = paginatedDatas,
                Categories = categories,
                Brands = brands,
                SortText= sortValue
            };

            return View(model);
        }


        //Filter Methods


        [HttpGet]
        public async Task<IActionResult> Filter(int minValue, int maxValue,int page = 1, int take = 12)
        {

            List<ProductVM> productsByFilter = await _productService.FilterAsync(minValue, maxValue);

            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductVM> paginatedDatas = new(productsByFilter, page, pageCount);

            List<CategoryVM> categories = await _categoryService.GetAllAsync();
            List<BrandVM> brands = await _brandService.GetAllAsync();


            int count = await _productService.GetProductCountAsync();

            ShopPageVM model = new()
            {
                PaginatedDatas = paginatedDatas,
                Categories = categories,
                Brands = brands,
                MaxValue=maxValue,
                MinValue=minValue
            };

            return View(model);

        }

    }
}
