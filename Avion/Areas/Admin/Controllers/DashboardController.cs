using Microsoft.AspNetCore.Mvc;

namespace Avion.Areas.Admin.Controllers
{
    public class DashboardController : MainController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
