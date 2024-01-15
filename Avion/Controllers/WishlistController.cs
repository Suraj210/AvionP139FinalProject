using Microsoft.AspNetCore.Mvc;

namespace Avion.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
