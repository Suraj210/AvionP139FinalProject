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
        Task<int> GetCountBySearch(string searchText);
        Task<List<ProductVM>> OrderByNameAsc(int page, int take);
        Task<List<ProductVM>> OrderByNameDesc(int page, int take);
        Task<List<ProductVM>> OrderByPriceAsc(int page, int take);
        Task<List<ProductVM>> OrderByPriceDesc(int page, int take);
        Task<List<ProductVM>> OrderByDate(int page, int take);
        Task<List<ProductVM>> FilterAsync(int minValue, int maxValue);
        Task<List<ProductVM>> GetLoadedProductsAsync(int skipCount, int take);
        Task<List<ProductVM>> GetPaginatedDatasWithIgnoreQuerryAsync(int page, int take);
        Task<int> GetCountWithIgnoreFilterAsync();
        Task<ProductVM> GetByIdIgnoreAsync(int id);
        Task SoftDeleteAsync(ProductVM request);
        Task CreateAsync(ProductCreateVM request);
        Task<ProductVM> GetByNameWithoutTrackingAsync(string name);
        Task DeleteAsync(int id);
    }
}
