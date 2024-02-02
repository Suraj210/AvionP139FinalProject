using Avion.Areas.Admin.ViewModels.Review;

namespace Avion.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewVM>> GetAllAsync();
        Task<ReviewVM> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task<List<ReviewVM>> GetReviewsByProductAsync(int id);
        Task CreateReview(ReviewCreateVM request, int? id, string userId);
    }
}
