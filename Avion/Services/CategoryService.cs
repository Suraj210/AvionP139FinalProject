using AutoMapper;
using Avion.Areas.Admin.ViewModels.Category;
using Avion.Data;
using Avion.Services.Interfaces;
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
    }
}
