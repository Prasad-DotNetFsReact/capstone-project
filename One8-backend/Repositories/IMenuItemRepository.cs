using One8_backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace One8_backend.Repositories
{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync();
        Task<MenuItem> GetMenuItemByIdAsync(int id);
        Task AddMenuItemAsync(MenuItem menuItem);
        Task UpdateMenuItemAsync(MenuItem menuItem);
        Task SoftDeleteMenuItemAsync(int id);
    }
}
