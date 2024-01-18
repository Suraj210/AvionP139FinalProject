using Avion.Areas.Admin.ViewModels.Blog;

namespace Avion.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogVM>> GetAllByTakeAsync(int take);
    }
}
