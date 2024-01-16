using AutoMapper;
using Avion.Areas.Admin.ViewModels.Testimonial;
using Avion.Data;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class TestimonialService : ITestimonialService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TestimonialService(AppDbContext context,
                           IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TestimonialVM>> GetAllAsync()
        {
            List<Testimonial> datas = await _context.Testimonials.ToListAsync();

            return _mapper.Map<List<TestimonialVM>>(datas);
        }
    }
}
