using Avion.Areas.Admin.ViewModels.Product;

namespace Avion.Services.Interfaces
{
    public interface IProductService
    {
        Task<int> GetProductCountAsync();
        Task<List<ProductVM>> GetAllAsync();
        Task<List<ProductVM>> GetAllByTakeAsync(int take);
        Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take);
    }
}
