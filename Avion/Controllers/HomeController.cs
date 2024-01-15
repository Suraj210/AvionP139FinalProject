using Microsoft.AspNetCore.Mvc;

namespace Avion.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

        
    }
}