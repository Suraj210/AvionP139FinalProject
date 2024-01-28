using AutoMapper;
using Avion.Areas.Admin.ViewModels.Blog;
using Avion.Areas.Admin.ViewModels.Brand;
using Avion.Areas.Admin.ViewModels.Category;
using Avion.Data;
using Avion.Helpers.Extentions;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public BrandService(AppDbContext context,
                             IMapper mapper,
                             IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        public async Task<List<BrandVM>> GetAllAsync()
        {
            List<Brand> brands = await _context.Brands.ToListAsync();

            return _mapper.Map<List<BrandVM>>(brands);
        }

        public async Task<BrandVM> GetByIdAsync(int id)
        {
            var data = await _context.Brands.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<BrandVM>(data);
        }


        // Brand Methods for Admin Panel


        //Get List Brands with its all datas in paginated format with Ignore Queryy Filters
        public async Task<List<BrandVM>> GetPaginatedDatasWithIgnoreQuerryAsync(int page, int take)
        {
            List<Brand> brands = await _context.Brands.IgnoreQueryFilters()
                                                      .Include(m => m.BrandCategories)
                                                      .ThenInclude(c => c.Category)
                                                      .Skip((page * take) - take)
                                                      .Take(take)
                                                      .ToListAsync();


            return _mapper.Map<List<BrandVM>>(brands);
        }

        //Get Brand Count with Ignore Querry Filters
        public async Task<int> GetCountWithIgnoreFilterAsync()
        {
            return await _context.Brands.IgnoreQueryFilters().CountAsync();
        }



        //Get particular Brand by its Id with Ignore QueryFilters
        public async Task<BrandVM> GetByIdIgnoreAsync(int id)
        {
            Brand brand = await _context.Brands.IgnoreQueryFilters()
                                               .Where(m => m.Id == id)
                                               .Include(m => m.BrandCategories)
                                               .ThenInclude(m => m.Category)
                                               .FirstOrDefaultAsync();

            return _mapper.Map<BrandVM>(brand);

        }



        //Soft delete Brand 
        public async Task SoftDeleteAsync(BrandVM request)
        {


            if (request.SoftDeleted)
            {
                request.SoftDeleted = false;
            }
            else
            {
                request.SoftDeleted = true;
            }

            Brand dbBrand = await _context.Brands.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == request.Id);
            _mapper.Map(request, dbBrand);
            _context.Brands.Update(dbBrand);
            await _context.SaveChangesAsync();
        }


        //To check Exist data by name
        public async Task<BrandVM> GetByNameWithoutTrackingAsync(string name)
        {
            Brand brand = await _context.Brands.Where(m => m.Name.Trim().ToLower() == name.Trim().ToLower()).FirstOrDefaultAsync();

            return _mapper.Map<BrandVM>(brand);
        }


        //Create Brand
        public async Task CreateAsync(BrandCreateVM request)
        {

            string fileName = $"{Guid.NewGuid()}-{request.Photo.FileName}";

            string path = _env.GetFilePath("assets/images", fileName);


            var selectedCategories = request.Categories.Where(m => m.Selected).Select(m => m.Value).ToList();

            var dbCategory = new Brand()
            {
                Name = request.Name,
                Image = fileName,
            };

            foreach (var item in selectedCategories)
            {
                dbCategory.BrandCategories.Add(new BrandCategory()
                {
                    CategoryId = int.Parse(item)
                });
            }

            await _context.Brands.AddAsync(dbCategory);
            await _context.SaveChangesAsync();
            await request.Photo.SaveFileAsync(path);

        }



        //Delete Brand

        public async Task DeleteAsync(int id)
        {
            Brand dbBrand = await _context.Brands.IgnoreQueryFilters()
                                                 .Where(m => m.Id == id)
                                                 .Include(m => m.BrandCategories)
                                                 .ThenInclude(m => m.Category)
                                                 .FirstOrDefaultAsync();


            _context.Brands.Remove(dbBrand);
            await _context.SaveChangesAsync();
            string path = _env.GetFilePath("assets/images", dbBrand.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

    }
}
