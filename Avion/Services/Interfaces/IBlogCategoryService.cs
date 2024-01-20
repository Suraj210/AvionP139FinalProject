using Avion.Areas.Admin.ViewModels.BlogCategory;

namespace Avion.Services.Interfaces
{
    public interface IBlogCategoryService
    {
        Task<List<BlogCategoryVM>> GetAllAsync();
        Task<BlogCategoryVM> GetByIdAsync(int id);
    }
}
