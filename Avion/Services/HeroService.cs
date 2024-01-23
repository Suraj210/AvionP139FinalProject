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
    public class HeroService : IHeroService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;


        public HeroService(AppDbContext context,
                           IMapper mapper,
                           IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<HeroVM>> GetAllAsync()
        {
           List<Hero> datas = await _context.Heros.ToListAsync();
            return _mapper.Map<List<HeroVM>>(datas);
        }

        public async Task<HeroVM> GetByIdAsync(int id)
        {
            var datas = await _context.Heros.FirstOrDefaultAsync(m => m.Id == id);
            HeroVM hero = _mapper.Map<HeroVM>(datas);
            return hero;
        }

        public async Task EditAsync(HeroEditVM request)
        {
            string fileName;

            if(request.Photo is not null)
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



            Hero dbHero = await _context.Heros.FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbHero);

            dbHero.Image = fileName;

            _context.Heros.Update(dbHero);
            await _context.SaveChangesAsync();

        }

    }
}
