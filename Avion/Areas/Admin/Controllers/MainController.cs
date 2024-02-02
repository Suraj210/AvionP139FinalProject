using Avion.Helpers.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Areas.Admin.Controllers
{

    [Area("Admin")]
    //[Authorize(Roles = "SuperAdmin, Admin")]
    public class MainController : Controller
    {
        
    }
}
