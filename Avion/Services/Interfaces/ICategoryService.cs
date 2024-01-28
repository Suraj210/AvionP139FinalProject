using Avion.Areas.Admin.ViewModels.Category;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Avion.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetAllAsync();
        Task<CategoryVM> GetByIdAsync(int id);
        List<SelectListItem> GetAllSelectedAsync();
    }
}
