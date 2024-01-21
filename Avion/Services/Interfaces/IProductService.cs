using Avion.Areas.Admin.ViewModels.Product;

namespace Avion.Services.Interfaces
{
    public interface IProductService
    {
        Task<int> GetProductCountAsync();
        Task<List<ProductVM>> GetAllAsync();
        Task<List<ProductVM>> GetAllByTakeAsync(int take);
        Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take);
        Task<int> GetCountByCategoryAsync(int id);
        Task<ProductVM> GetByIdAsync(int id);
        Task<List<ProductVM>> GetPaginatedDatasByCategoryAsync(int id, int page, int take);
        Task<List<ProductVM>> GetPaginatedDatasByBrandAsync(int id, int page, int take);
        Task<int> GetCountByBrandAsync(int id);
        Task<List<ProductVM>> SearchAsync(string searchText, int page, int take);
    }
}
