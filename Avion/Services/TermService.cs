using AutoMapper;
using Avion.Areas.Admin.ViewModels.Terms;
using Avion.Data;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class TermService : ITermService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TermService(AppDbContext context,
                                   IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TermsVM>> GetAllAsync()
        {
            var datas = await _context.Terms.ToListAsync();
            return _mapper.Map<List<TermsVM>>(datas);
        }
    }
}
