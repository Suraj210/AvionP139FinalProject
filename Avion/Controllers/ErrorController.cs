using Microsoft.AspNetCore.Mvc;

namespace Avion.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    {
                        ViewBag.Title = "Bad Request";
                        ViewBag.ErrorMessage = "Something Went Wrong";
                        break;
                    }
                case 401:
                    {
                        ViewBag.Title = "Unauthorized";
                        ViewBag.ErrorMessage = "You are lacking proper authentication credentials";
                        break;
                    }

                case 403:
                    {
                        ViewBag.Title = "Forbidden";
                        ViewBag.ErrorMessage = "You don't have permission for this action";
                        break;
                    }

                case 404:
                    {
                        ViewBag.Title = "Not Found";
                        ViewBag.ErrorMessage = "The page you are looking for can not be found";
                        break;
                    }

                case 405:
                    {
                        ViewBag.Title = "Method Not Allowed";
                        ViewBag.ErrorMessage = "This action is not allowed";
                        break;
                    }

                case 500:
                    {
                        ViewBag.Title = "Internal Server Error";
                        ViewBag.ErrorMessage = "Internal Server Error";
                        break;
                    }
            }

            return View(statusCode);
        }
    }
}
