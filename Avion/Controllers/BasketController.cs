using Microsoft.AspNetCore.Mvc;

namespace Avion.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
