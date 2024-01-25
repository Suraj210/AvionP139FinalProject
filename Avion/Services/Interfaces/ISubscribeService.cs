using Avion.Areas.Admin.ViewModels.Subscribe;

namespace Avion.Services.Interfaces
{
    public interface ISubscribeService
    {
        Task<List<SubscribeVM>> GetAllAsync();
        Task<SubscribeVM> GetByEmailAsync(string email);
        Task CreateAsync(SubscribeCreateVM vm);
        Task DeleteAsync(int id);
    }
}
