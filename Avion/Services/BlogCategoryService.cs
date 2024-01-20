using AutoMapper;
using Avion.Areas.Admin.ViewModels.BlogCategory;
using Avion.Data;
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
    }
}
