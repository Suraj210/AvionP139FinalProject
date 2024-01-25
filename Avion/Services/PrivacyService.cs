using AutoMapper;
using Avion.Areas.Admin.ViewModels.Privacy;
using Avion.Data;
using Avion.Models;
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

        public async Task<PrivacyVM> GetByIdAsync(int id)
        {
            var datas = await _context.Privacies.FirstOrDefaultAsync(m => m.Id == id);
            PrivacyVM privacy = _mapper.Map<PrivacyVM>(datas);
            return privacy;
        }

        public async Task CreateAsync(PrivacyCreateVM request)
        {
            var data = _mapper.Map<Privacy>(request);

            await _context.AddAsync(data);

            await _context.SaveChangesAsync();

        }

        public async Task EditAsync(PrivacyEditVM request)
        {

            Privacy dbPrivacy = await _context.Privacies.FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbPrivacy);

            dbPrivacy.CreateTime = DateTime.Now;

            _context.Privacies.Update(dbPrivacy);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            Privacy privacy = await _context.Privacies.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Privacies.Remove(privacy);
            await _context.SaveChangesAsync();

        }
    }
}
