using Avion.Areas.Admin.ViewModels.Contact;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Areas.Admin.Controllers
{
    public class ContactMessageController : MainController
    {
        private readonly IContactService _contactService;

        public ContactMessageController(IContactService contactService)
        {
            _contactService = contactService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _contactService.GetAllMessagesAsync());
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            ContactMessageVM dbMessage = await _contactService.GetMessageByIdAsync((int)id);

            if (dbMessage is null) return NotFound();

            return View(dbMessage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
