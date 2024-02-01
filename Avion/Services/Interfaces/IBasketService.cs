using Avion.Areas.Admin.ViewModels.Product;
using Avion.Helpers.Responses;
using Avion.Models;
using Avion.ViewModels.Basket;

namespace Avion.Services.Interfaces
{
    public interface IBasketService
    {
        void AddBasket(int id, ProductVM product);
        int GetCount();
        Task<List<BasketDetailVM>> GetBasketDatasAsync();
        Task<CountPlusAndMinus> PlusIcon(int id);
        Task<CountPlusAndMinus> MinusIcon(int id);
        Task<DeleteBasketItemResponse> DeleteItem(int id);
        List<BasketVM> GetDatasFromCoockies();
        Task<Basket> GetByUserIdAsync(string userId);
        Task<List<BasketProduct>> GetAllByBasketIdAsync(int? basketId);
    }
}
