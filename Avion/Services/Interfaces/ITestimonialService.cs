using Avion.Areas.Admin.ViewModels.Testimonial;

namespace Avion.Services.Interfaces
{
    public interface ITestimonialService
    {
        Task<List<TestimonialVM>> GetAllAsync();
    }
}
