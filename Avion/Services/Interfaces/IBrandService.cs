using Avion.Areas.Admin.ViewModels.Brand;

namespace Avion.Services.Interfaces
{
    public interface IBrandService
    {
        Task<List<BrandVM>> GetAllAsync();
    }
}
