using AutoMapper;
using Avion.Areas.Admin.ViewModels.Feature;
using Avion.Data;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FeatureService(AppDbContext context,
                           IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<FeatureVM>> GetAllAsync()
        {
            List<Feature> datas = await _context.Features.ToListAsync();

            return _mapper.Map<List<FeatureVM>>(datas);
        }
    }
}
