using Avion.Areas.Admin.ViewModels.Tag;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Avion.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<TagVM>> GetAllAsync();
        Task<List<TagVM>> GetAllIgnoreAdminAsync();
        Task<TagVM> GetByIdIgnoreAsync(int id);
        Task<TagVM> GetByIdWithoutTrackingAsync(int id);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(TagVM request);
        Task CreateAsync(TagCreateVM tag);
        Task EditAsync(TagEditVM tag);
        Task<TagVM> GetByNameWithoutTrackingAsync(string name);
        List<SelectListItem> GetAllSelectedAsync();
    }
}
