using Microsoft.EntityFrameworkCore;
using One8_backend.Data;
using One8_backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace One8_backend.Repositories
{
      public class MenuItemRepository : IMenuItemRepository
       {
        private readonly One8DbContext _context;

        public MenuItemRepository(One8DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync()
        {
            var q = (from x in _context.MenuItems
                     join y in _context.Restaurants on x.RestaurantId equals y.Id
                     where x.IsDeleted == false && y.IsDeleted == false
                     select x

).ToList();
            return q;
        }

        public async Task<MenuItem> GetMenuItemByIdAsync(int id)
        {
            return await _context.MenuItems
                .Include(m => m.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItemsByRestaurantIdAsync(int restaurantId)
        {
            return await _context.MenuItems
                .Where(m => m.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task AddMenuItemAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteMenuItemAsync(int id)
        {
            var menuitem = await GetMenuItemByIdAsync(id);
            if (menuitem != null)
            {
                menuitem.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
