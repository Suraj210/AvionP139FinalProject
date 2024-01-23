using Avion.Areas.Admin.ViewModels.Hero;

namespace Avion.Services.Interfaces
{
    public interface IHeroService
    {
        Task<List<HeroVM>> GetAllAsync();
        Task<HeroVM> GetByIdAsync(int id);
        Task EditAsync(HeroEditVM request);
    }
}
