using Avion.Areas.Admin.ViewModels.Advert;

namespace Avion.Services.Interfaces
{
    public interface IAdvertService
    {
        Task<List<AdvertVM>> GetAllAsync();
    }
}
