using AutoMapper;
using Avion.Areas.Admin.ViewModels.BlogCategory;
using Avion.Areas.Admin.ViewModels.Category;
using Avion.Data;
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
    }
}
