using Avion.Areas.Admin.ViewModels.Testimonial;

namespace Avion.Services.Interfaces
{
    public interface ITestimonialService
    {
        Task<List<TestimonialVM>> GetAllAsync();
        Task<TestimonialVM> GetByIdAsync(int id);

        Task EditAsync(TestimonialEditVM request);
        Task SoftDeleteAsync(TestimonialVM request);
        Task<TestimonialVM> GetByIdIgnoreAsync(int id);
        Task<List<TestimonialVM>> GetAllIgnoreAdminAsync();
        Task DeleteAsync(int id);
    }
}
