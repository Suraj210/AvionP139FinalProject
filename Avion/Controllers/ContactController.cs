using Avion.Areas.Admin.ViewModels.Contact;
using Avion.Services.Interfaces;
using Avion.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        public ContactController( IContactService contactService)
        {
            _contactService = contactService;

        }

        public IActionResult Index()
        {
            var contact = _contactService.GetData();

            ContactPageVM model = new()
            {
                Contact = contact

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMessage(ContactMessageCreateVM request)
        {

            await _contactService.CreateAsync(request);

            return RedirectToAction("Index", "Contact");

        }
    }
}
