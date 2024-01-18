using Avion.Areas.Admin.ViewModels.Tag;

namespace Avion.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<TagVM>> GetAllAsync();
    }
}
