using AutoMapper;
using Avion.Areas.Admin.ViewModels.BlogCategory;
using Avion.Areas.Admin.ViewModels.Brand;
using Avion.Areas.Admin.ViewModels.Category;
using Avion.Data;
using Avion.Helpers.Extentions;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext context,
                                   IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<CategoryVM>> GetAllAsync()
        {
            var datas = await _context.Categories.ToListAsync();

            return _mapper.Map<List<CategoryVM>>(datas);
        }
        public async Task<CategoryVM> GetByIdAsync(int id)
        {
            var data = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<CategoryVM>(data);
        }


        //Methods for Admin Panel
        public List<SelectListItem> GetAllSelectedAsync()
        {
            return _context.Categories.Select(m => new SelectListItem()
            {
                Text = m.Name,
                Value = m.Id.ToString(),

            }).ToList();
        }


        //Get List Categories with its all datas in paginated format with Ignore Queryy Filters
        public async Task<List<CategoryVM>> GetPaginatedDatasWithIgnoreQuerryAsync(int page, int take)
        {
            List<Category> categories = await _context.Categories.IgnoreQueryFilters()
                                                                 .Include(m => m.BrandCategories)
                                                                 .ThenInclude(c => c.Brand)
                                                                 .Skip((page * take) - take)
                                                                 .Take(take)
                                                                 .ToListAsync();


            return _mapper.Map<List<CategoryVM>>(categories);
        }
        //Get Category Count with Ignore Querry Filters
        public async Task<int> GetCountWithIgnoreFilterAsync()
        {
            return await _context.Categories.IgnoreQueryFilters().CountAsync();
        }
        //Get particular Category by its Id with Ignore QueryFilters
        public async Task<CategoryVM> GetByIdIgnoreAsync(int id)
        {
            Category category = await _context.Categories.IgnoreQueryFilters()
                                               .Where(m => m.Id == id)
                                               .Include(m => m.BrandCategories)
                                               .ThenInclude(m => m.Brand)
                                               .FirstOrDefaultAsync();

            return _mapper.Map<CategoryVM>(category);

        }
        //Soft delete Brand 
        public async Task SoftDeleteAsync(CategoryVM request)
        {


            if (request.SoftDeleted)
            {
                request.SoftDeleted = false;
            }
            else
            {
                request.SoftDeleted = true;
            }

            Category dbCategory = await _context.Categories.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == request.Id);
            _mapper.Map(request, dbCategory);
            _context.Categories.Update(dbCategory);
            await _context.SaveChangesAsync();
        }
        //To check Exist data by name
        public async Task<CategoryVM> GetByNameWithoutTrackingAsync(string name)
        {
            Category category = await _context.Categories.Where(m => m.Name.Trim().ToLower() == name.Trim().ToLower()).FirstOrDefaultAsync();

            return _mapper.Map<CategoryVM>(category);
        }
        //Create Category
        public async Task CreateAsync(CategoryCreateVM request)
        {

            var selectedBrands = request.Brands.Where(m => m.Selected).Select(m => m.Value).ToList();

            var dbBrand = new Category()
            {
                Name = request.Name,
            };

            foreach (var item in selectedBrands)
            {
                dbBrand.BrandCategories.Add(new BrandCategory()
                {
                    BrandId = int.Parse(item)
                });
            }

            await _context.Categories.AddAsync(dbBrand);
            await _context.SaveChangesAsync();

        }
        //Delete Category
        public async Task DeleteAsync(int id)
        {
            Category dbCategory = await _context.Categories.IgnoreQueryFilters()
                                                 .Where(m => m.Id == id)
                                                 .Include(m => m.BrandCategories)
                                                 .ThenInclude(m => m.Brand)
                                                 .FirstOrDefaultAsync();


            _context.Categories.Remove(dbCategory);
            await _context.SaveChangesAsync();
        }
        //Edit Category
        public async Task EditAsync(CategoryEditVM request)
        {

            Category categoryById = await _context.Categories.IgnoreQueryFilters().Include(m => m.BrandCategories).FirstOrDefaultAsync(m => m.Id == request.Id);

            var existingIds = categoryById.BrandCategories.Select(m => m.BrandId).ToList();

            var selectedIds = request.Brands.Where(m => m.Selected).Select(m => m.Value).Select(int.Parse).ToList();

            var toAdd = selectedIds.Except(existingIds);
            var toRemove = existingIds.Except(selectedIds);

            categoryById.BrandCategories = categoryById.BrandCategories.Where(m => !toRemove.Contains(m.BrandId)).ToList();

            foreach (var item in toAdd)
            {
                categoryById.BrandCategories.Add(new BrandCategory
                {
                    BrandId = item
                });
            }



            _mapper.Map(request, categoryById);

            _context.Categories.Update(categoryById);

            await _context.SaveChangesAsync();
        }

    }
}
