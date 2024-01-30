using AutoMapper;
using Avion.Areas.Admin.ViewModels.Product;
using Avion.Data;
using Avion.Helpers.Extentions;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public ProductService(AppDbContext context,
                             IMapper mapper,
                             IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        public async Task<List<ProductVM>> GetAllAsync()
        {
            List<Product> products = await _context.Products.Include(b => b.Brand)
                                                            .Include(m => m.Category)
                                                            .Include(m => m.Images)
                                                            .ToListAsync();

            return _mapper.Map<List<ProductVM>>(products);
        }
        public async Task<List<ProductVM>> GetAllByTakeAsync(int take)
        {
            List<Product> products = await _context.Products.Include(m => m.Images)
                                                            .Take(take)
                                                            .ToListAsync();

            return _mapper.Map<List<ProductVM>>(products);
        }
        public async Task<int> GetProductCountAsync()
        {
            return await _context.Products.CountAsync();
        }
        public async Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take)
        {
            List<Product> products = await _context.Products.OrderByDescending(m => m.CreateTime)
                                                            .Include(m => m.Images)
                                                            .Skip((page * take) - take)
                                                            .Take(take)
                                                            .ToListAsync();
            return _mapper.Map<List<ProductVM>>(products);
        }
        //Get Product Count by Category
        public async Task<int> GetCountByCategoryAsync(int id)
        {
            return await _context.Products.Where(m => m.CategoryId == id).CountAsync();
        }
        //Get particular Product by its Id
        public async Task<ProductVM> GetByIdAsync(int id)
        {
            Product data = await _context.Products.Include(m => m.Images)
                                                  .FirstOrDefaultAsync(m => m.Id == id);

            return _mapper.Map<ProductVM>(data);

        }
        //Get list of Products by categoryname
        public async Task<List<ProductVM>> GetPaginatedDatasByCategoryAsync(int id, int page, int take)
        {
            List<Product> products = await _context.Products.Where(m => m.CategoryId == id)
                                                            .OrderByDescending(m => m.CreateTime)
                                                            .Include(m => m.Images)
                                                            .Skip((page * take) - take)
                                                            .Take(take)
                                                            .ToListAsync();
            return _mapper.Map<List<ProductVM>>(products);
        }
        //Get Product Count by Brand
        public async Task<int> GetCountByBrandAsync(int id)
        {
            return await _context.Products.Where(m => m.BrandId == id).CountAsync();
        }
        //Get list of Products by BrandName
        public async Task<List<ProductVM>> GetPaginatedDatasByBrandAsync(int id, int page, int take)
        {
            List<Product> products = await _context.Products.Where(m => m.BrandId == id)
                                                            .OrderByDescending(m => m.Price)
                                                            .Include(m => m.Images)
                                                            .Skip((page * take) - take)
                                                            .Take(take)
                                                            .ToListAsync();
            return _mapper.Map<List<ProductVM>>(products);
        }
        //Search 
        public async Task<List<ProductVM>> SearchAsync(string searchText, int page, int take)
        {
            var dbProducts = await _context.Products.Where(m => m.Name.ToLower().Trim().Contains(searchText.ToLower().Trim()))
                                                    .Include(m => m.Images)
                                                    .OrderByDescending(m => m.Id)
                                                    .Skip((page * take) - take)
                                                    .Take(take)
                                                    .ToListAsync();

            return _mapper.Map<List<ProductVM>>(dbProducts);
        }
        public async Task<int> GetCountBySearch(string searchText)
        {
            return await _context.Products.Where(m => m.Name.ToLower().Trim().Contains(searchText.ToLower().Trim()))
                                          .Include(m => m.Images)
                                          .OrderByDescending(m => m.Id)
                                          .CountAsync();

        }
        //Sort
        public async Task<List<ProductVM>> OrderByNameAsc(int page, int take)
        {
            var datas = await _context.Products.Include(m => m.Images)
                                               .OrderBy(p => p.Name)
                                               .Skip((page * take) - take)
                                               .Take(take)
                                               .ToListAsync();

            return _mapper.Map<List<ProductVM>>(datas);


        }
        public async Task<List<ProductVM>> OrderByNameDesc(int page, int take)
        {
            var datas = await _context.Products.Include(m => m.Images)
                                               .OrderByDescending(p => p.Name)
                                               .Skip((page * take) - take)
                                               .Take(take)
                                               .ToListAsync();

            return _mapper.Map<List<ProductVM>>(datas);


        }
        public async Task<List<ProductVM>> OrderByPriceAsc(int page, int take)
        {
            var datas = await _context.Products.Include(m => m.Images)
                                               .OrderBy(p => p.Price)
                                               .Skip((page * take) - take)
                                               .Take(take)
                                               .ToListAsync();

            return _mapper.Map<List<ProductVM>>(datas);


        }
        public async Task<List<ProductVM>> OrderByPriceDesc(int page, int take)
        {
            var datas = await _context.Products.Include(m => m.Images)
                                               .OrderByDescending(p => p.Price)
                                               .Skip((page * take) - take)
                                               .Take(take)
                                               .ToListAsync();

            return _mapper.Map<List<ProductVM>>(datas);


        }
        public async Task<List<ProductVM>> OrderByDate(int page, int take)
        {
            var datas = await _context.Products.Include(m => m.Images)
                                               .OrderByDescending(p => p.CreateTime)
                                               .Skip((page * take) - take)
                                               .Take(take)
                                               .ToListAsync();

            return _mapper.Map<List<ProductVM>>(datas);


        }
        //Filter
        public async Task<List<ProductVM>> FilterAsync(int minValue, int maxValue)
        {
            List<Product> products = await _context.Products.Include(m => m.Images).Where(x => x.Price >= minValue && x.Price <= maxValue).ToListAsync();
            return _mapper.Map<List<ProductVM>>(products);

        }
        public async Task<int> FilterCountAsync(int minValue, int maxValue)
        {
            return await _context.Products.Include(m => m.Images).Where(x => x.Price >= minValue && x.Price <= maxValue).CountAsync();
        }
        //Show More
        public async Task<List<ProductVM>> GetLoadedProductsAsync(int skipCount, int take)
        {
            List<Product> products = await _context.Products.Include(m => m.Images)
                                                            .Skip(skipCount)
                                                            .Take(take)
                                                            .ToListAsync();

            return _mapper.Map<List<ProductVM>>(products);

        }
        // For ADMIN Panel
        public async Task<ProductVM> GetByNameWithoutTrackingAsync(string name)
        {
            Product product = await _context.Products.Where(m => m.Name.Trim().ToLower() == name.Trim().ToLower()).FirstOrDefaultAsync();

            return _mapper.Map<ProductVM>(product);
        }
        //Get List Products with its all datas in paginated format with Ignore Queryy Filters
        public async Task<List<ProductVM>> GetPaginatedDatasWithIgnoreQuerryAsync(int page, int take)
        {
            List<Product> products = await _context.Products.IgnoreQueryFilters()
                                                   .OrderByDescending(m => !m.SoftDeleted)
                                                   .Include(c => c.Category)
                                                   .Include(c => c.Brand)
                                                   .Include(p => p.Images)
                                                   .Skip((page * take) - take)
                                                   .Take(take)
                                                   .ToListAsync();
            return _mapper.Map<List<ProductVM>>(products);
        }
        //Get Product Count with Ignore Querry Filters
        public async Task<int> GetCountWithIgnoreFilterAsync()
        {
            return await _context.Products.IgnoreQueryFilters().CountAsync();
        }
        //Get particular Product by its Id with Ignore QueryFilters
        public async Task<ProductVM> GetByIdIgnoreAsync(int id)
        {
            Product product = await _context.Products.IgnoreQueryFilters()
                                                     .Where(m => m.Id == id)
                                                     .Include(c => c.Category)
                                                     .Include(c => c.Brand)
                                                     .Include(p => p.Images)
                                                     .FirstOrDefaultAsync();

            if (product.Category.SoftDeleted)
            {
                product.Category = null;
            }

            if (product.Brand.SoftDeleted)
            {
                product.Brand = null;
            }

            return _mapper.Map<ProductVM>(product);

        }
        //Soft delete Product 
        public async Task SoftDeleteAsync(ProductVM request)
        {


            if (request.SoftDeleted)
            {
                request.SoftDeleted = false;
            }
            else
            {
                request.SoftDeleted = true;
            }

            Product dbProduct = await _context.Products.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == request.Id);
            _mapper.Map(request, dbProduct);
            _context.Products.Update(dbProduct);
            await _context.SaveChangesAsync();
        }
        //Create Product
        public async Task CreateAsync(ProductCreateVM request)
        {
            List<ProductImage> newImages = new();

            foreach (var photo in request.Photos)
            {
                string fileName = $"{Guid.NewGuid()}-{photo.FileName}";

                string path = _env.GetFilePath("assets/images/products", fileName);

                await photo.SaveFileAsync(path);

                newImages.Add(new ProductImage { Image = fileName });
            }

            newImages.FirstOrDefault().IsMain = true;

            await _context.ProductImages.AddRangeAsync(newImages);


            var data = _mapper.Map<Product>(request);

            data.Images = newImages;



            await _context.Products.AddAsync(data);
            await _context.SaveChangesAsync();

        }
        //Delete Product
        public async Task DeleteAsync(int id)
        {
            Product dbData = await _context.Products.IgnoreQueryFilters()
                                                    .Where(m => m.Id == id)
                                                    .Include(c => c.Category)
                                                    .Include(c => c.Brand)
                                                    .Include(p => p.Images)
                                                    .FirstOrDefaultAsync();


            _context.Products.Remove(dbData);
            await _context.SaveChangesAsync();




            foreach (var item in dbData.Images)
            {

            string path = _env.GetFilePath("assets/images/products", item.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            }




        }
    }
}
