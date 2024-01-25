using AutoMapper;
using Avion.Areas.Admin.ViewModels.Privacy;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Areas.Admin.Controllers
{
    public class PrivacyController : MainController
    {
        private readonly IPrivacyService _privacyService;
        private readonly IMapper _mapper;

        public PrivacyController(IPrivacyService privacyService, IMapper mapper)
        {
            _privacyService = privacyService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _privacyService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            PrivacyVM privacy = await _privacyService.GetByIdAsync((int)id);

            if (privacy is null) return NotFound();

            return View(privacy);
        }




        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PrivacyCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            await _privacyService.CreateAsync(request);


            return RedirectToAction("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _privacyService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            PrivacyVM privacy = await _privacyService.GetByIdAsync((int)id);

            if (privacy is null) return NotFound();

            PrivacyEditVM privacyEditVm = _mapper.Map<PrivacyEditVM>(privacy);

            return View(privacyEditVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, PrivacyEditVM request)
        {
            if (id is null) return BadRequest();

            PrivacyVM dbPrivacy = await _privacyService.GetByIdAsync((int)id);

            if (dbPrivacy is null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _privacyService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
