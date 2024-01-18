using Avion.Areas.Admin.ViewModels.Subscribe;

namespace Avion.Services.Interfaces
{
    public interface ISubscribeService
    {
        Task<List<SubscribeVM>> GetAllAsync();
        Task CreateAsync(SubscribeCreateVM vm);
    }
}
