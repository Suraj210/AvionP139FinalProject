using AutoMapper;
using Avion.Areas.Admin.ViewModels.Hero;
using Avion.Areas.Admin.ViewModels.Idea;
using Avion.Helpers.Extentions;
using Avion.Services;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Areas.Admin.Controllers
{
    public class IdeaController : MainController
    {
        private readonly IIdeaService _ideaService;
        private readonly IMapper _mapper;

        public IdeaController(IIdeaService ideaService,
                              IMapper mapper)
        {
            _ideaService = ideaService;
            _mapper = mapper;   
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _ideaService.GetAsync());
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            IdeaVM idea = await _ideaService.GetByIdAsync((int)id);

            if (idea is null) return NotFound();

            return View(idea);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            IdeaVM idea = await _ideaService.GetByIdAsync((int)id);

            if (idea is null) return NotFound();

            IdeaEditVM ideaEditVm = _mapper.Map<IdeaEditVM>(idea);


            return View(ideaEditVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> Edit(int? id, IdeaEditVM request)
        {

            if (id is null) return BadRequest();

            IdeaVM dbIdea = await _ideaService.GetByIdAsync((int)id);

            if (dbIdea is null) return NotFound();


            request.Image = dbIdea.Image;

            if (!ModelState.IsValid)
            {
                return View(request);
            }



            if (request.Photo is not null)
            {
                if (!request.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File can be only image format");
                    return View(request);
                }

                if (!request.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "File size can  be max 500 kb");
                    return View(request);
                }
            }



            await _ideaService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }

    }
}
