using AutoMapper;
using Avion.Areas.Admin.ViewModels.Advert;
using Avion.Data;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class AdvertService : IAdvertService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AdvertService(AppDbContext context,
                           IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<AdvertVM>> GetAllAsync()
        {
            List<Advert> datas = await _context.Adverts.ToListAsync();

            return _mapper.Map<List<AdvertVM>>(datas); 
        }
    }
}
