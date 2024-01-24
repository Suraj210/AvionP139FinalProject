using Avion.Areas.Admin.ViewModels.Feature;

namespace Avion.Services.Interfaces
{
    public interface IFeatureService
    {
        Task<List<FeatureVM>> GetAllAsync();
        Task<FeatureVM> GetByIdAsync(int id);
        Task EditAsync(FeatureEditVM request);
    }
}
