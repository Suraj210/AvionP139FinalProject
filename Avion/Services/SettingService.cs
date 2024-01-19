using AutoMapper;
using Avion.Data;
using Avion.Services.Interfaces;

namespace Avion.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public SettingService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.AsEnumerable()
                                    .ToDictionary(m => m.Key, m => m.Value);
        }
    }
}
