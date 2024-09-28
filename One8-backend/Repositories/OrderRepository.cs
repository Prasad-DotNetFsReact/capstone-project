using Microsoft.EntityFrameworkCore;
using One8_backend.Data;
using One8_backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace One8_backend.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly One8DbContext _context;

        public OrderRepository(One8DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem) // Assuming OrderItem has a MenuItem navigation property
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteOrderAsync(int id)
        {
            var order = await GetOrderByIdAsync(id);
            if (order != null)
            {
                order.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}

