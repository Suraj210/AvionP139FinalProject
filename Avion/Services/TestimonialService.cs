using AutoMapper;
using Avion.Areas.Admin.ViewModels.Hero;
using Avion.Areas.Admin.ViewModels.Testimonial;
using Avion.Data;
using Avion.Helpers.Extentions;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class TestimonialService : ITestimonialService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public TestimonialService(AppDbContext context,
                           IMapper mapper,
                           IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<TestimonialVM>> GetAllAsync()
        {
            List<Testimonial> datas = await _context.Testimonials.ToListAsync();

            return _mapper.Map<List<TestimonialVM>>(datas);
        }

        public async Task<List<TestimonialVM>> GetAllIgnoreAdminAsync()
        {
            List<Testimonial> datas = await _context.Testimonials.IgnoreQueryFilters().ToListAsync();

            return _mapper.Map<List<TestimonialVM>>(datas);
        }

        public async Task<TestimonialVM> GetByIdAsync(int id)
        {
            var datas = await _context.Testimonials.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);
            TestimonialVM testimonial = _mapper.Map<TestimonialVM>(datas);
            return testimonial;
        }

        public async Task<TestimonialVM> GetByIdIgnoreAsync(int id)
        {
            var datas = await _context.Testimonials.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == id);
            TestimonialVM testimonial = _mapper.Map<TestimonialVM>(datas);
            return testimonial;
        }


        public async Task EditAsync(TestimonialEditVM request)
        {
            string fileName;

            if (request.Photo is not null)
            {

                string oldPath = _env.GetFilePath("assets/images/", request.Image);

                fileName = $"{Guid.NewGuid()} - {request.Photo.FileName}";

                string newPath = _env.GetFilePath("assets/images/", fileName);


                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }

                await request.Photo.SaveFileAsync(newPath);
            }
            else
            {
                fileName = request.Image;
            }



            Testimonial dbTestimonial = await _context.Testimonials.FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbTestimonial);

            dbTestimonial.Image = fileName;
            dbTestimonial.CreateTime = DateTime.Now;

            _context.Testimonials.Update(dbTestimonial);
            await _context.SaveChangesAsync();

        }




        public async Task SoftDeleteAsync(TestimonialVM request)
        {
            //int count = await _context.Testimonials.IgnoreQueryFilters().Where(m => m.SoftDeleted).CountAsync();


            if (request.SoftDeleted)
            {
                request.SoftDeleted = false;
            }
            else
            {
                request.SoftDeleted = true;
            }

            Testimonial dbTestimonial = await _context.Testimonials.IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == request.Id);
            _mapper.Map(request, dbTestimonial);
            _context.Testimonials.Update(dbTestimonial);
            await _context.SaveChangesAsync();
        }
    
    
        public async Task DeleteAsync(int id)
        {
            Testimonial testimonial = await _context.Testimonials.IgnoreQueryFilters().Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Testimonials.Remove(testimonial);
            await _context.SaveChangesAsync();

            string path = _env.GetFilePath("assets/images/", testimonial.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    
    }
}
