using Avion.Services.Interfaces;
using Avion.ViewModels.Wishlist;
using Microsoft.AspNetCore.Mvc;

namespace Avion.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.WishlistCount = _wishlistService.GetCount();
            return View(await _wishlistService.GetWishlistDatasAsync());
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _wishlistService.DeleteItem(id);
            List<WishlistVM> wishlist = _wishlistService.GetDatasFromCoockies();

            return Ok(wishlist.Count);
        }
    }
}
