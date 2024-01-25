using AutoMapper;
using Avion.Areas.Admin.ViewModels.About;
using Avion.Areas.Admin.ViewModels.Hero;
using Avion.Data;
using Avion.Helpers.Extentions;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AboutService(AppDbContext context,
                                   IMapper mapper,
                                   IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public  async Task<List<AboutVM>> GetAllAsync()
        {
            var datas = await _context.Abouts.ToListAsync();

            return _mapper.Map<List<AboutVM>>(datas);
        }


        public async Task<AboutVM> GetByIdAsync(int id)
        {
            var datas = await _context.Abouts.FirstOrDefaultAsync(m => m.Id == id);
            AboutVM about = _mapper.Map<AboutVM>(datas);
            return about;
        }

        public async Task EditAsync(AboutEditVM request)
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



            About dbAbout = await _context.Abouts.FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbAbout);

            dbAbout.Image = fileName;
            dbAbout.CreateTime = DateTime.Now;

            _context.Abouts.Update(dbAbout);
            await _context.SaveChangesAsync();

        }
    }
}
