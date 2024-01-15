using Microsoft.AspNetCore.Mvc;

namespace Avion.Controllers
{
    public class TermsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
