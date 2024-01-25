using AutoMapper;
using Avion.Areas.Admin.ViewModels.About;
using Avion.Areas.Admin.ViewModels.Hero;
using Avion.Helpers.Extentions;
using Avion.Services;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Areas.Admin.Controllers
{
    public class AboutController : MainController
    {
        private readonly IAboutService _aboutService;
        private readonly IMapper _mapper;

        public AboutController(IAboutService aboutService,
                              IMapper mapper)
        {
            _aboutService = aboutService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _aboutService.GetAllAsync());
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            AboutVM about = await _aboutService.GetByIdAsync((int)id);

            if (about is null) return NotFound();

            return View(about);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            AboutVM about = await _aboutService.GetByIdAsync((int)id);

            if (about is null) return NotFound();

            AboutEditVM aboutEditVm = _mapper.Map<AboutEditVM>(about);


            return View(aboutEditVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> Edit(int? id, AboutEditVM request)
        {
            if (id is null) return BadRequest();

            AboutVM dbAbout = await _aboutService.GetByIdAsync((int)id);

            if (dbAbout is null) return NotFound();


            request.Image = dbAbout.Image;

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



            await _aboutService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
