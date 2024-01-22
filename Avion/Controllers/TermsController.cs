using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Controllers
{
    public class TermsController : Controller
    {
        private readonly ITermService _termService;

        public TermsController(ITermService termService)
        {
            _termService = termService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _termService.GetAllAsync();
            return View(model);

        }
    }
}
