using Avion.Areas.Admin.ViewModels.Privacy;

namespace Avion.Services.Interfaces
{
    public interface IPrivacyService
    {
        Task<List<PrivacyVM>> GetAllAsync();
        Task<PrivacyVM> GetByIdAsync(int id);
        Task CreateAsync(PrivacyCreateVM request);
        Task EditAsync(PrivacyEditVM request);
        Task DeleteAsync(int id);
    }
}

