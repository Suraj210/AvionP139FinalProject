using AutoMapper;
using Avion.Areas.Admin.ViewModels.Privacy;
using Avion.Data;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class PrivacyService : IPrivacyService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PrivacyService(AppDbContext context,
                                   IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<PrivacyVM>> GetAllAsync()
        {
            var datas = await _context.Privacies.ToListAsync();
            return _mapper.Map<List<PrivacyVM>>(datas);
        }
    }
}
