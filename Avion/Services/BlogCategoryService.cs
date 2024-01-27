using AutoMapper;
using Avion.Areas.Admin.ViewModels.BlogCategory;
using Avion.Areas.Admin.ViewModels.Tag;
using Avion.Data;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BlogCategoryService(AppDbContext context,
                                   IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BlogCategoryVM>> GetAllAsync()
        {
            var datas = await _context.BlogCategories.ToListAsync();

            return _mapper.Map<List<BlogCategoryVM>>(datas);
        }

        public async Task<BlogCategoryVM> GetByIdAsync(int id)
        {
            var data = await _context.BlogCategories.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<BlogCategoryVM>(data);
        }


        public async Task<List<BlogCategoryVM>> GetAllIgnoreAdminAsync()
        {
            return _mapper.Map<List<BlogCategoryVM>>(await _context.BlogCategories.IgnoreQueryFilters().ToListAsync());
        }

        public async Task<BlogCategoryVM> GetByIdIgnoreAsync(int id)
        {
            var datas = await _context.BlogCategories.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);
            BlogCategoryVM blogCategory = _mapper.Map<BlogCategoryVM>(datas);
            return blogCategory;
        }

        public async Task CreateAsync(BlogCategoryCreateVM blogCategory)
        {
            BlogCategory dbBlogCategory = _mapper.Map<BlogCategory>(blogCategory);

            await _context.BlogCategories.AddAsync(dbBlogCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            BlogCategory dbBlogCategory = await _context.BlogCategories.IgnoreQueryFilters().Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.BlogCategories.Remove(dbBlogCategory);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(BlogCategoryVM request)
        {


            if (request.SoftDeleted)
            {
                request.SoftDeleted = false;
            }
            else
            {
                request.SoftDeleted = true;
            }

            BlogCategory dbBlogCategory = await _context.BlogCategories.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == request.Id);
            _mapper.Map(request, dbBlogCategory);
            _context.BlogCategories.Update(dbBlogCategory);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(BlogCategoryEditVM request)
        {
            BlogCategory dbBlogCategory = await _context.BlogCategories.IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id);

            _mapper.Map(request, dbBlogCategory);

            _context.BlogCategories.Update(dbBlogCategory);

            await _context.SaveChangesAsync();
        }
        public async Task<BlogCategoryVM> GetByIdWithoutTrackingAsync(int id)
        {
            return _mapper.Map<BlogCategoryVM>(await _context.BlogCategories.IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync(m => m.Id == id));
        }

        public async Task<BlogCategoryVM> GetByNameWithoutTrackingAsync(string name)
        {
            return _mapper.Map<BlogCategoryVM>(await _context.BlogCategories.IgnoreQueryFilters().AsNoTracking()
                                                         .FirstOrDefaultAsync(m => m.Name.Trim().ToLower() == name.Trim().ToLower()));
        }
    }
}
