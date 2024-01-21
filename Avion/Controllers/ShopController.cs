using Avion.Areas.Admin.ViewModels.Brand;
using Avion.Areas.Admin.ViewModels.Category;
using Avion.Areas.Admin.ViewModels.Product;
using Avion.Helpers;
using Avion.Services;
using Avion.Services.Interfaces;
using Avion.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Index(int page = 1, int take = 12)
        {
            List<ProductVM> dbPaginatedDatas = await _productService.GetPaginatedDatasAsync(page, take);

            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);
            List<CategoryVM> categories =await _categoryService.GetAllAsync();
            List<BrandVM> brands =await _brandService.GetAllAsync();

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

        public IActionResult ProductDetail()
        {
            return View();
        }
    }
}
