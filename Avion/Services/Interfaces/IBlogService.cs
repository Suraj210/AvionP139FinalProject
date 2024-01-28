using Avion.Areas.Admin.ViewModels.Blog;

namespace Avion.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogVM>> GetAllByTakeWithCategoryAsync(int take);
        Task<List<BlogVM>> GetPaginatedDatasAsync(int page, int take);
        Task<List<BlogVM>> GetPaginatedDatasByCategoryAsync(int id, int page, int take);
        Task<int> GetCountAsync();
        Task<int> GetCountByCategoryAsync(int id);
        Task<BlogVM> GetByIdAsync(int id);
        Task<BlogVM> GetByIdIgnoreAsync(int id);
        Task<BlogVM> GetByNameWithoutTrackingAsync(string name);
        Task SoftDeleteAsync(BlogVM request);
        Task<List<BlogVM>> GetPaginatedDatasWithIgnoreQuerryAsync(int page, int take);
        Task<int> GetCountWithIgnoreFilterAsync();
        Task CreateAsync(BlogCreateVM blog);
        Task DeleteAsync(int id);
        Task EditAsync(BlogEditVM blog);
    }
}
