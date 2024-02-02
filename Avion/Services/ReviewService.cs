using AutoMapper;
using Avion.Areas.Admin.ViewModels.Account;
using Avion.Areas.Admin.ViewModels.Review;
using Avion.Data;
using Avion.Models;
using Avion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avion.Services
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public ReviewService(AppDbContext context,
                                 IMapper mapper,
                                 IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        public async Task<List<ReviewVM>> GetAllAsync()
        {
            return _mapper.Map<List<ReviewVM>>(await _context.Reviews.ToListAsync());
        }

        public async Task<ReviewVM> GetByIdAsync(int id)
        {
            return _mapper.Map<ReviewVM>(await _context.Reviews.FirstOrDefaultAsync(m => m.Id == id));
        }

        public async Task DeleteAsync(int id)
        {
            Review review = await _context.Reviews.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }


        public async Task<List<ReviewVM>> GetReviewsByProductAsync(int id)
        {
            List<ReviewVM> model = new();

            var reviews = await _context.Reviews
                         .Where(m => m.ProductId == id)
                         .ToListAsync();




            foreach (var review in reviews)
            {
                model.Add(new ReviewVM
                {
                    Id = review.Id,
                    Name = review.Name,
                    Email = review.Email,
                    Title = review.Title,
                    Message = review.Message

                });
            }

            return model;

        }
    

        public async Task CreateReview(ReviewCreateVM request, int? id, string userId)
        {

            Review review = new()
            {
                Name = request.Name,
                Email = request.Email,
                Title = request.Title,
                Message = request.Message,
                ProductId = (int)id,
                AppUserId = userId
            };

            review.CreateTime= DateTime.Now;
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }
    
    
    }
}
