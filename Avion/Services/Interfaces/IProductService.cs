using Avion.Areas.Admin.ViewModels.Product;

namespace Avion.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllAsync();
        Task<List<ProductVM>> GetAllByTakeAsync(int take);
    }
}
