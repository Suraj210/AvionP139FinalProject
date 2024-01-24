using AutoMapper;
using Avion.Areas.Admin.ViewModels.Feature;
using Avion.Areas.Admin.ViewModels.Hero;
using Avion.Helpers.Extentions;
using Avion.Services;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Areas.Admin.Controllers
{
    public class FeatureController : MainController
    {
        private readonly IFeatureService _featureService;
        private readonly IMapper _mapper;

        public FeatureController(IFeatureService featureService,
                                 IMapper mapper)
        {
            _featureService = featureService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _featureService.GetAllAsync());
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            FeatureVM feature = await _featureService.GetByIdAsync((int)id);

            if (feature is null) return NotFound();

            return View(feature);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            FeatureVM feature = await _featureService.GetByIdAsync((int)id);

            if (feature is null) return NotFound();

            FeatureEditVM featureEditVm = _mapper.Map<FeatureEditVM>(feature);


            return View(featureEditVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> Edit(int? id, FeatureEditVM request)
        {
            if (id is null) return BadRequest();

            FeatureVM dbFeature= await _featureService.GetByIdAsync((int)id);

            if (dbFeature is null) return NotFound();


            request.Image = dbFeature.Image;

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



            await _featureService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
