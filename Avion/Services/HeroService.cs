using AutoMapper;
using Avion.Areas.Admin.ViewModels.Hero;
using Avion.Data;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class HeroService : IHeroService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HeroService(AppDbContext context,
                           IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<HeroVM>> GetAllAsync()
        {
           List<Hero> datas = await _context.Heros.ToListAsync();
            return _mapper.Map<List<HeroVM>>(datas);
        }
    }
}
