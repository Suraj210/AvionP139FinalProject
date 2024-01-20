using AutoMapper;
using Avion.Areas.Admin.ViewModels.Blog;
using Avion.Data;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;


        public BlogService(AppDbContext context,
                             IMapper mapper,
                             IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        //Get Blogs with its Category by Take function
        public async Task<List<BlogVM>> GetAllByTakeWithCategoryAsync(int take)
        {
            List<Blog> blogs = await _context.Blogs.OrderByDescending(m => m.CreateTime)
                                                 .Take(take)
                                                 .Include(m => m.BlogCategory)
                                                 .ToListAsync();

            return _mapper.Map<List<BlogVM>>(blogs);
        }


        //Get List Blogs with its all datas in paginated format
        public async Task<List<BlogVM>> GetPaginatedDatasAsync(int page, int take)
        {
            List<Blog> blogs = await _context.Blogs.OrderByDescending(m => m.CreateTime)
                                                   .Include(bc => bc.BlogCategory)
                                                   .Include(m => m.BlogTags)
                                                   .ThenInclude(m => m.Tag)
                                                   .Skip((page * take) - take)
                                                   .Take(take)
                                                   .ToListAsync();
            return _mapper.Map<List<BlogVM>>(blogs);
        }

        //Get Blog Count
        public async Task<int> GetCountAsync()
        {
            return await _context.Blogs.CountAsync();
        }


        //Get Blog Count by Category
        public async Task<int> GetCountByCategoryAsync(int id)
        {
            return await _context.Blogs.Where(m=>m.BlogCategoryId == id).CountAsync();
        }

        //Get particular Blog by its Id
        public async Task<BlogVM> GetByIdAsync(int id)
        {
            Blog blog = await _context.Blogs.Where(m => m.Id == id)
                                            .Include(bc => bc.BlogCategory)
                                            .Include(m => m.BlogTags)
                                            .ThenInclude(m => m.Tag)
                                            .FirstOrDefaultAsync();

            return _mapper.Map<BlogVM>(blog);

        }

        //Get list of Blogs by categoryname

        public async Task<List<BlogVM>> GetPaginatedDatasByCategoryAsync(int id, int page, int take)
        {
            List<Blog> blogs = await _context.Blogs.Where(m=>m.BlogCategoryId == id)
                                                   .OrderByDescending(m => m.CreateTime)
                                                   .Include(bc => bc.BlogCategory)
                                                   .Include(m => m.BlogTags)
                                                   .ThenInclude(m => m.Tag)
                                                   .Skip((page * take) - take)
                                                   .Take(take)
                                                   .ToListAsync();
            return _mapper.Map<List<BlogVM>>(blogs);
        }
    }
}
