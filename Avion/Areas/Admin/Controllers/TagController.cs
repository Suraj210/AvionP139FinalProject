using Avion.Areas.Admin.ViewModels.Tag;
using Avion.Areas.Admin.ViewModels.Testimonial;
using Avion.Services;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Areas.Admin.Controllers
{
    public class TagController : MainController
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _tagService.GetAllIgnoreAdminAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(TagCreateVM request)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }

            TagVM existTag = await _tagService.GetByNameWithoutTrackingAsync(request.Name);

            if (existTag != null)
            {
                ModelState.AddModelError("Name", "This Tag is already exists");
                return View();
            }


            await _tagService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _tagService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete(int id)
        {
            TagVM tag = await _tagService.GetByIdIgnoreAsync(id);

            await _tagService.SoftDeleteAsync(tag);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            TagVM dbTag = await _tagService.GetByIdWithoutTrackingAsync((int)id);

            if (dbTag is null) return NotFound();

            return View(new TagEditVM
            {
                Name = dbTag.Name
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int? id, TagEditVM request)
        {
            if (id is null) return BadRequest();

            TagVM dbTag = await _tagService.GetByIdWithoutTrackingAsync((int)id);

            if (dbTag is null) return NotFound();


            if (!ModelState.IsValid)
            {
                return View();
            }

            TagVM existTag = await _tagService.GetByNameWithoutTrackingAsync(request.Name);



            if (existTag != null)
            {
                if (existTag.Id == request.Id)
                {
                    await _tagService.EditAsync(request);

                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Name", "This Tag is already exists");
                return View();
            }

            await _tagService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }
    }
}
