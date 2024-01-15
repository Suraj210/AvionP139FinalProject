using Microsoft.AspNetCore.Mvc;

namespace Avion.Controllers
{
    public class PrivacyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
