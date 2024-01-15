using Microsoft.AspNetCore.Mvc;

namespace Avion.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
