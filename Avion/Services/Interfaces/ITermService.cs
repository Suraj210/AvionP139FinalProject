using Avion.Areas.Admin.ViewModels.Feature;
using Avion.Areas.Admin.ViewModels.Terms;
using Avion.Areas.Admin.ViewModels.Testimonial;

namespace Avion.Services.Interfaces
{
    public interface ITermService
    {
        Task<List<TermsVM>> GetAllAsync();
        Task<TermsVM> GetByIdAsync(int id);
        Task CreateAsync(TermsCreateVM request);
        Task EditAsync(TermsEditVM request);
        Task DeleteAsync(int id);
    }
}
