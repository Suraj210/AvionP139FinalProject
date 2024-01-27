using Avion.Areas.Admin.ViewModels.BlogCategory;

namespace Avion.Services.Interfaces
{
    public interface IBlogCategoryService
    {
        Task<List<BlogCategoryVM>> GetAllAsync();
        Task<BlogCategoryVM> GetByIdAsync(int id);
        Task<List<BlogCategoryVM>> GetAllIgnoreAdminAsync();
        Task<BlogCategoryVM> GetByIdIgnoreAsync(int id);
        Task CreateAsync(BlogCategoryCreateVM blogCategory);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(BlogCategoryVM request);
        Task EditAsync(BlogCategoryEditVM request);
        Task<BlogCategoryVM> GetByIdWithoutTrackingAsync(int id);
        Task<BlogCategoryVM> GetByNameWithoutTrackingAsync(string name);
    }
}
