using AutoMapper;
using Avion.Areas.Admin.ViewModels.Hero;
using Avion.Areas.Admin.ViewModels.Idea;
using Avion.Data;
using Avion.Helpers.Extentions;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class IdeaService : IIdeaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public IdeaService(AppDbContext context,
                           IMapper mapper,
                           IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }


        public async Task<IdeaVM> GetAsync()
        {
            Idea data = _context.Ideas.FirstOrDefault();

            return _mapper.Map<IdeaVM>(data);
        }

        public async Task<IdeaVM> GetByIdAsync(int id)
        {
            var datas = await _context.Ideas.FirstOrDefaultAsync(m => m.Id == id);
            IdeaVM idea = _mapper.Map<IdeaVM>(datas);
            return idea;
        }

        public async Task EditAsync(IdeaEditVM request)
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



            Idea dbIdea = await _context.Ideas.FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbIdea);

            dbIdea.Image = fileName;
            dbIdea.CreateTime = DateTime.Now;

            _context.Ideas.Update(dbIdea);
            await _context.SaveChangesAsync();

        }
    }
}
