using Microsoft.EntityFrameworkCore;
using One8_backend.Data;
using One8_backend.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace One8_backend.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly One8DbContext _context;

        public RestaurantRepository(One8DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant> GetRestaurantByIdAsync(int id)
        {
            return await _context.Restaurants
                .Include(r => r.MenuItems)
                .Include(r => r.Reviews)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddRestaurantAsync(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRestaurantAsync(Restaurant restaurant)
        {
            _context.Entry(restaurant).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteRestaurantAsync(int id)
        {
            var restaurant = await GetRestaurantByIdAsync(id);
            if (restaurant != null)
            {
                restaurant.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
