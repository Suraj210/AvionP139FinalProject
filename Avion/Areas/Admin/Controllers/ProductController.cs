using AutoMapper;
using Avion.Areas.Admin.ViewModels.Blog;
using Avion.Areas.Admin.ViewModels.Brand;
using Avion.Areas.Admin.ViewModels.Category;
using Avion.Areas.Admin.ViewModels.Product;
using Avion.Data;
using Avion.Helpers;
using Avion.Helpers.Extentions;
using Avion.Models;
using Avion.Services;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Avion.Areas.Admin.Controllers
{
    public class ProductController : MainController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;


        public ProductController(IProductService productService,
                                 ICategoryService categoryService,
                                 IBrandService brandService,
                                 AppDbContext context,
                                 IWebHostEnvironment env,
                                 IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _context = context;
            _env = env;
            _mapper = mapper;
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

                if (!photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photos", "File size can be max 500kb.");
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


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.productCategories = await GetCategoriesAsync();
            ViewBag.productBrands = await GetBrandsAsync();

            if (id is null) return BadRequest();

            Product dbData = await _context.Products.AsNoTracking()
                                                    .IgnoreQueryFilters()
                                                    .Where(m => m.Id == id)
                                                    .Include(c => c.Category)
                                                    .Include(c => c.Brand)
                                                    .Include(p => p.Images)
                                                    .FirstOrDefaultAsync();

            if (dbData is null) return NotFound();

            return View(new ProductEditVM
            {
                Id = dbData.Id,
                Name = dbData.Name,
                Description = dbData.Description,
                CategoryId = (int)dbData.CategoryId,
                BrandId = (int)dbData.BrandId,
                Price = dbData.Price,
                Images = dbData.Images.ToList(),
                Width = dbData.Width,
                Height = dbData.Height,
                Weight = dbData.Weight,
                Length = dbData.Length,
                Material = dbData.Material,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, ProductEditVM request)
        {
            if (id is null) return BadRequest();

            ViewBag.productCategories = await GetCategoriesAsync();
            ViewBag.productBrands = await GetBrandsAsync();

            Product dbData = await _context.Products
                                                               .IgnoreQueryFilters()
                                                               .Where(m => m.Id == id)
                                                               .Include(c => c.Category)
                                                               .Include(c => c.Brand)
                                                               .Include(p => p.Images)
                                                               .FirstOrDefaultAsync();
            if (dbData is null) return NotFound();


            request.Images = dbData.Images.ToList();

            dbData.CreateTime = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return View(request);
            }



            List<ProductImage> newImages = new();
            if (request.Photos != null)
            {
                foreach (var photo in request.Photos)
                {

                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photos", "File can be only image format");
                        return View(request);
                    }

                    if (!photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photos", "File size can be max 500 kb");
                        return View(request);
                    }
                }

                foreach (var photo in request.Photos)
                {
                    string fileName = $"{Guid.NewGuid()}-{photo.FileName}";

                    string path = _env.GetFilePath("assets/images/products", fileName);

                    await photo.SaveFileAsync(path);

                    newImages.Add(new ProductImage { Image = fileName });
                }

                await _context.ProductImages.AddRangeAsync(newImages);
            }

            newImages.AddRange(request.Images);

            dbData.Images = newImages;
            dbData.Id = request.Id;
            dbData.Name = request.Name;
            dbData.Description = request.Description;
            dbData.CategoryId = request.CategoryId;
            dbData.BrandId = request.BrandId;
            dbData.Price = request.Price;
            dbData.Width = request.Width;
            dbData.Height = request.Height;
            dbData.Weight = request.Weight;
            dbData.Length = request.Length;
            dbData.Material = request.Material;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            ProductImage image = await _context.ProductImages.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.ProductImages.Remove(image);

            await _context.SaveChangesAsync();

            string path = _env.GetFilePath("assets/images/products", image.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            return Ok();
        }

    }
}
