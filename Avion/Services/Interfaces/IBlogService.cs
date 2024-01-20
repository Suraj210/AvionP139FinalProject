using Avion.Areas.Admin.ViewModels.Blog;

namespace Avion.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogVM>> GetAllByTakeWithCategoryAsync(int take);
        Task<List<BlogVM>> GetPaginatedDatasAsync(int page, int take);
        Task<int> GetCountAsync();
        Task<BlogVM> GetByIdAsync(int id);
    }
}
