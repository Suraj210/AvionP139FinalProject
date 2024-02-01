using Avion.Areas.Admin.ViewModels.Product;
using Avion.Models;
using Avion.ViewModels.Wishlist;

namespace Avion.Services.Interfaces
{
    public interface IWishlistService
    {
        int AddWishlist(int id, ProductVM product);
        int GetCount();
        Task<List<WishlistDetailVM>> GetWishlistDatasAsync();
        void DeleteItem(int id);
        List<WishlistVM> GetDatasFromCoockies();
        void SetDatasToCookies(List<WishlistVM> wishlist, Product dbProduct, WishlistVM existProduct);
        Task<Wishlist> GetByUserIdAsync(string userId);
        Task<List<WishlistProduct>> GetAllByWishlistIdAsync(int? wishlistId);
    }
}
