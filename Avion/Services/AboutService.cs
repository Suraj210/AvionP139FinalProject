using AutoMapper;
using Avion.Areas.Admin.ViewModels.About;
using Avion.Data;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AboutService(AppDbContext context,
                                   IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public  async Task<List<AboutVM>> GetAllAsync()
        {
            var datas = await _context.Abouts.ToListAsync();

            return _mapper.Map<List<AboutVM>>(datas);
        }
    }
}
