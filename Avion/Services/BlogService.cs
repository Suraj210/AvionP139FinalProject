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


        public async Task<List<BlogVM>> GetAllByTakeAsync(int take)
        {
            List<Blog> blogs = await _context.Blogs.OrderByDescending(m => m.CreateTime)
                                                 .Take(take)
                                                 .Include(m => m.BlogCategory)
                                                 .ToListAsync();

            return _mapper.Map<List<BlogVM>>(blogs);
        }
    }
}
