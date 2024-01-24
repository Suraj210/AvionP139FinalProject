using AutoMapper;
using Avion.Areas.Admin.ViewModels.Advert;
using Avion.Areas.Admin.ViewModels.Hero;
using Avion.Data;
using Avion.Helpers.Extentions;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class AdvertService : IAdvertService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AdvertService(AppDbContext context,
                           IMapper mapper,
                           IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        public async Task<List<AdvertVM>> GetAllAsync()
        {
            List<Advert> datas = await _context.Adverts.ToListAsync();

            return _mapper.Map<List<AdvertVM>>(datas); 
        }

        public async Task<AdvertVM> GetByIdAsync(int id)
        {
            var datas = await _context.Adverts.FirstOrDefaultAsync(m => m.Id == id);
            AdvertVM advert = _mapper.Map<AdvertVM>(datas);
            return advert;
        }

        public async Task EditAsync(AdvertEditVM request)
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



            Advert dbAdvert = await _context.Adverts.FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbAdvert);

            dbAdvert.Image = fileName;
            dbAdvert.CreateTime = DateTime.Now;

            _context.Adverts.Update(dbAdvert);
            await _context.SaveChangesAsync();

        }
    }
}
