using AutoMapper;
using Avion.Areas.Admin.ViewModels.Advert;
using Avion.Areas.Admin.ViewModels.Hero;
using Avion.Helpers.Extentions;
using Avion.Services;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Areas.Admin.Controllers
{
    public class AdvertController : MainController
    {
        private readonly IAdvertService _advertService;
        private readonly IMapper _mapper;

        public AdvertController(IAdvertService advertService,
                                IMapper mapper)
        {
            _advertService = advertService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _advertService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            AdvertVM advert = await _advertService.GetByIdAsync((int)id);

            if (advert is null) return NotFound();

            return View(advert);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            AdvertVM advert = await _advertService.GetByIdAsync((int)id);

            if (advert is null) return NotFound();

            AdvertEditVM advertEditVm = _mapper.Map<AdvertEditVM>(advert);


            return View(advertEditVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> Edit(int? id, AdvertEditVM request)
        {

            if (id is null) return BadRequest();

            AdvertVM dbAdvert = await _advertService.GetByIdAsync((int)id);

            if (dbAdvert is null) return NotFound();


            request.Image = dbAdvert.Image;

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



            await _advertService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }

    }
}
