using Microsoft.EntityFrameworkCore;
using One8_backend.Data;
using One8_backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace One8_backend.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly One8DbContext _context;

        public ReviewRepository(One8DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Restaurant)
                .ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Review>> GetReviewsByRestaurantIdAsync(int restaurantId)
        {
            return await _context.Reviews
                .Where(r => r.RestaurantId == restaurantId)
                .Include(r => r.User)
                .Include(r => r.Restaurant)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(int userId)
        {
            return await _context.Reviews
                .Where(r => r.UserId == userId)
                .Include(r => r.User)
                .Include(r => r.Restaurant)
                .ToListAsync();
        }

        public async Task AddReviewAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReviewAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }
        public async Task SoftDeleteReviewAsync(int id)
        {
            var review = await GetReviewByIdAsync(id);
            if (review != null)
            {
                review.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}

