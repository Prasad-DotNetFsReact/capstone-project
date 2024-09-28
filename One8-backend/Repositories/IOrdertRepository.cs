using One8_backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace One8_backend.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task SoftDeleteOrderAsync(int id);
    }
}

