using Avion.Areas.Admin.ViewModels.About;

namespace Avion.Services.Interfaces
{
    public interface IAboutService
    {
        Task<List<AboutVM>> GetAllAsync();
        Task<AboutVM> GetByIdAsync(int id);
        Task EditAsync(AboutEditVM request);
    }
}
