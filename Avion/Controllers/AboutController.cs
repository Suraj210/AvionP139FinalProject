using Microsoft.AspNetCore.Mvc;

namespace Avion.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
