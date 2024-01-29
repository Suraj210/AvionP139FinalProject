using Avion.Areas.Admin.ViewModels.Category;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Avion.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetAllAsync();
        Task<CategoryVM> GetByIdAsync(int id);
        List<SelectListItem> GetAllSelectedAsync();
        Task<List<CategoryVM>> GetPaginatedDatasWithIgnoreQuerryAsync(int page, int take);
        Task<int> GetCountWithIgnoreFilterAsync();
        Task<CategoryVM> GetByIdIgnoreAsync(int id);
        Task SoftDeleteAsync(CategoryVM request);
        Task CreateAsync(CategoryCreateVM request);
        Task<CategoryVM> GetByNameWithoutTrackingAsync(string name);
        Task DeleteAsync(int id);
        Task EditAsync(CategoryEditVM request);
    }
}
