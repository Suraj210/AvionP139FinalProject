using AutoMapper;
using Avion.Areas.Admin.ViewModels.Hero;
using Avion.Helpers.Extentions;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Areas.Admin.Controllers
{
    public class HeroController : MainController
    {
        private readonly IHeroService _heroService;
        private readonly IMapper _mapper;


        public HeroController(IHeroService heroService,
                             IMapper mapper)
        {
            _heroService = heroService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _heroService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            HeroVM hero = await _heroService.GetByIdAsync((int)id);

            if (hero is null) return NotFound();

            return View(hero);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            HeroVM hero = await _heroService.GetByIdAsync((int)id);

            if (hero is null) return NotFound();

            HeroEditVM heroEditVm = _mapper.Map<HeroEditVM>(hero);


            return View(heroEditVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

       
        public async Task<IActionResult> Edit(int? id, HeroEditVM request)
        {

            if (id is null) return BadRequest();

            HeroVM dbHero = await _heroService.GetByIdAsync((int) id);

            if (dbHero is null) return NotFound();


            request.Image = dbHero.Image;

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

                if (!request.Photo.CheckFileSize(1000))
                {
                    ModelState.AddModelError("Photo", "File size can  be max 1000 kb");
                    return View(request);
                }
            }
          


            await _heroService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
