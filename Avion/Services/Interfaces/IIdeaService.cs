using Avion.Areas.Admin.ViewModels.Idea;

namespace Avion.Services.Interfaces
{
    public interface IIdeaService
    {
        Task<IdeaVM> GetAsync();
        Task<IdeaVM> GetByIdAsync(int id);
        Task EditAsync(IdeaEditVM request);
    }
}
