using Avion.Areas.Admin.ViewModels.Blog;
using Avion.Areas.Admin.ViewModels.Brand;

namespace Avion.Services.Interfaces
{
    public interface IBrandService
    {
        Task<List<BrandVM>> GetAllAsync();
        Task<BrandVM> GetByIdAsync(int id);
        Task<List<BrandVM>> GetPaginatedDatasWithIgnoreQuerryAsync(int page, int take);
        Task<int> GetCountWithIgnoreFilterAsync();
        Task<BrandVM> GetByIdIgnoreAsync(int id);
        Task SoftDeleteAsync(BrandVM request);
        Task<BrandVM> GetByNameWithoutTrackingAsync(string name);
        Task CreateAsync(BrandCreateVM request);
        Task DeleteAsync(int id);
        Task EditAsync(BrandEditVM request);
    }
}
