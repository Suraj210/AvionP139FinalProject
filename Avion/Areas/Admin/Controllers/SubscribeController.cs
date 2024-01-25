using Avion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Areas.Admin.Controllers
{
    public class SubscribeController : MainController
    {
        private readonly ISubscribeService _subscribeService;


        public SubscribeController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _subscribeService.GetAllAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _subscribeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
