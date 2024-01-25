using AutoMapper;
using Avion.Areas.Admin.ViewModels.Terms;
using Avion.Data;
using Avion.Models;
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

        public async Task<TermsVM> GetByIdAsync(int id)
        {
            var datas = await _context.Terms.FirstOrDefaultAsync(m => m.Id == id);
            TermsVM term = _mapper.Map<TermsVM>(datas);
            return term;
        }

        public async Task CreateAsync(TermsCreateVM request)
        {
            var data = _mapper.Map<Term>(request);

            await _context.AddAsync(data);

            await _context.SaveChangesAsync();

        }

        public async Task EditAsync(TermsEditVM request)
        {

            Term dbTerm = await _context.Terms.FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbTerm);

            dbTerm.CreateTime = DateTime.Now;

            _context.Terms.Update(dbTerm);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            Term term = await _context.Terms.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Terms.Remove(term);
            await _context.SaveChangesAsync();
        
        }

     

    }
}
