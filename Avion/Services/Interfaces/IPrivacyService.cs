using Avion.Areas.Admin.ViewModels.Privacy;

namespace Avion.Services.Interfaces
{
    public interface IPrivacyService
    {
        Task<List<PrivacyVM>> GetAllAsync();
    }
}
