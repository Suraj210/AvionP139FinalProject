using Microsoft.AspNetCore.Mvc;

namespace Avion.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
