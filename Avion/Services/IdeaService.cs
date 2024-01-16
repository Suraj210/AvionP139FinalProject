using AutoMapper;
using Avion.Areas.Admin.ViewModels.Idea;
using Avion.Data;
using Avion.Models;
using Avion.Services.Interfaces;

namespace Avion.Services
{
    public class IdeaService : IIdeaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public IdeaService(AppDbContext context,
                           IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IdeaVM> GetAsync()
        {
            Idea data = _context.Ideas.FirstOrDefault();

            return _mapper.Map<IdeaVM>(data);
        }
    }
}
