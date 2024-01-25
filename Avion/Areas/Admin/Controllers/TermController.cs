using AutoMapper;
using Avion.Areas.Admin.ViewModels.Terms;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Areas.Admin.Controllers
{
    public class TermController : MainController
    {
        private readonly ITermService _termService;
        private readonly IMapper _mapper;

        public TermController(ITermService termService,
                              IMapper mapper)
        {
            _mapper = mapper;
            _termService = termService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _termService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            TermsVM term = await _termService.GetByIdAsync((int)id);

            if (term is null) return NotFound();

            return View(term);
        }




        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TermsCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            await _termService.CreateAsync(request);


            return RedirectToAction("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _termService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            TermsVM term = await _termService.GetByIdAsync((int)id);

            if (term is null) return NotFound();

            TermsEditVM termEditVm = _mapper.Map<TermsEditVM>(term);

            return View(termEditVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, TermsEditVM request)
        {

            if (id is null) return BadRequest();

            TermsVM dbTerm = await _termService.GetByIdAsync((int)id);

            if (dbTerm is null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _termService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }

    }
}
