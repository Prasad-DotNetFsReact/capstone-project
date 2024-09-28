using Microsoft.EntityFrameworkCore;
using One8_backend.Data;
using One8_backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace One8_backend.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly One8DbContext _context;

        public OrderItemRepository(One8DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
            return await _context.OrderItems
                .Include(oi => oi.MenuItem)
                .Include(oi => oi.Order)
                .ToListAsync();
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            return await _context.OrderItems
                .Include(oi => oi.MenuItem)
                .Include(oi => oi.Order)
                .FirstOrDefaultAsync(oi => oi.Id == id);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .Include(oi => oi.MenuItem)
                .Include(oi => oi.Order)
                .ToListAsync();
        }

        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteOrderItemAsync(int id)
        {
            var orderItem = await GetOrderItemByIdAsync(id);
            if (orderItem != null)
            {
                orderItem.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
