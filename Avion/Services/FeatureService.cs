using AutoMapper;
using Avion.Areas.Admin.ViewModels.Feature;
using Avion.Areas.Admin.ViewModels.Hero;
using Avion.Data;
using Avion.Helpers.Extentions;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public FeatureService(AppDbContext context,
                           IMapper mapper,
                           IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task<List<FeatureVM>> GetAllAsync()
        {
            List<Feature> datas = await _context.Features.ToListAsync();

            return _mapper.Map<List<FeatureVM>>(datas);
        }

        public async Task<FeatureVM> GetByIdAsync(int id)
        {
            var datas = await _context.Features.FirstOrDefaultAsync(m => m.Id == id);
            FeatureVM feature = _mapper.Map<FeatureVM>(datas);
            return feature;
        }

        public async Task EditAsync(FeatureEditVM request)
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



            Feature dbFeature = await _context.Features.FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbFeature);

            dbFeature.Image = fileName;
            dbFeature.CreateTime = DateTime.Now;

            _context.Features.Update(dbFeature);
            await _context.SaveChangesAsync();

        }
    }
}
