using AutoMapper;
using Avion.Areas.Admin.ViewModels.Blog;
using Avion.Areas.Admin.ViewModels.Product;
using Avion.Data;
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
            List<Product> products = await _context.Products.Include(b=>b.Brand)
                                                            .Include(m => m.Category)
                                                            .Include(m => m.Images)
                                                            .ToListAsync();

            return _mapper.Map<List<ProductVM>>(products);
        }

        public async Task<List<ProductVM>> GetAllByTakeAsync(int take)
        {
            List<Product> products = await _context.Products.Include(m => m.Images)
                                                            .Include(b=>b.Brand)
                                                            .Include(m => m.Category)
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
            List<Product> products = await _context.Products.OrderByDescending(m => m.Price)
                                                            .Include(m => m.Category)
                                                            .Include(b=>b.Brand)
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
            Product data = await _context.Products.Include(m => m.Category)
                                                  .Include(b=>b.Brand)
                                                  .Include(m => m.Images)
                                                  .FirstOrDefaultAsync(m => m.Id == id);

            return _mapper.Map<ProductVM>(data);

        }

        //Get list of Products by categoryname

        public async Task<List<ProductVM>> GetPaginatedDatasByCategoryAsync(int id, int page, int take)
        {
            List<Product> products = await _context.Products.Where(m=>m.CategoryId==id)
                                                            .OrderByDescending(m => m.Price)
                                                            .Include(m => m.Category)
                                                            .Include(b => b.Brand)
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
                                                            .Include(m => m.Category)
                                                            .Include(b => b.Brand)
                                                            .Include(m => m.Images)
                                                            .Skip((page * take) - take)
                                                            .Take(take)
                                                            .ToListAsync();
            return _mapper.Map<List<ProductVM>>(products);
        }

    }
}
