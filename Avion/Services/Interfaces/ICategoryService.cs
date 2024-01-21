using Avion.Areas.Admin.ViewModels.Category;

namespace Avion.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetAllAsync();
    }
}
