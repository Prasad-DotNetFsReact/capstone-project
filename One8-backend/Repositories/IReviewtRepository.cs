using One8_backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace One8_backend.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int id);
        Task<IEnumerable<Review>> GetReviewsByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<Review>> GetReviewsByUserIdAsync(int userId);
        Task AddReviewAsync(Review review);
        Task UpdateReviewAsync(Review review);
        Task SoftDeleteReviewAsync(int id);
    }
}

