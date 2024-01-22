using Avion.Areas.Admin.ViewModels.Terms;

namespace Avion.Services.Interfaces
{
    public interface ITermService
    {
        Task<List<TermsVM>> GetAllAsync();
    }
}
