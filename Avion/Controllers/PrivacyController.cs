using Avion.Services;
using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Controllers
{

  
    public class PrivacyController : Controller
    {
        private readonly IPrivacyService _privacyService;

        public PrivacyController(IPrivacyService privacyService)
        {
            _privacyService = privacyService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _privacyService.GetAllAsync();
            return View(model);
        }
    }
}
