using AutoMapper;
using Avion.Areas.Admin.ViewModels.Hero;
using Avion.Areas.Admin.ViewModels.Testimonial;
using Avion.Helpers.Extentions;
using Avion.Services;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Avion.Areas.Admin.Controllers
{
    public class TestimonialController : MainController
    {
        private readonly ITestimonialService _testimonialService;
        private readonly IMapper _mapper;

        public TestimonialController(ITestimonialService testimonialService,
                                     IMapper mapper)
        {
            _mapper = mapper;
            _testimonialService = testimonialService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete(int id)
        {
            TestimonialVM testimonial = await _testimonialService.GetByIdAsync(id);

            await _testimonialService.SoftDeleteAsync(testimonial);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _testimonialService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _testimonialService.GetAllIgnoreAdminAsync();
            return View(data);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            TestimonialVM testimonial = await _testimonialService.GetByIdIgnoreAsync((int)id);

            if (testimonial is null) return NotFound();

            return View(testimonial);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            TestimonialVM testimonial = await _testimonialService.GetByIdAsync((int)id);

            if (testimonial is null) return NotFound();

            TestimonialEditVM testimonialEditVm = _mapper.Map<TestimonialEditVM>(testimonial);


            return View(testimonialEditVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, TestimonialEditVM request)
        {

            if (id is null) return BadRequest();

            TestimonialVM dbTestimonial = await _testimonialService.GetByIdAsync((int)id);

            if (dbTestimonial is null) return NotFound();


            request.Image = dbTestimonial.Image;
            request.IsMain =dbTestimonial.IsMain;

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



            await _testimonialService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestiimonialCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (!request.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File can be only image format");
                return View();
            }

            if (!request.Photo.CheckFileSize(500))
            {
                ModelState.AddModelError("Photo", "File size can be max 500 kb");
                return View(request);
            }



            await _testimonialService.CreateAsync(request);


            return RedirectToAction("Index");
        }

    }
}
