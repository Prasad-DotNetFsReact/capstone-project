using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using One8_backend.Models;
using One8_backend.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace One8_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemController(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems()
        {
            var orderItems = await _orderItemRepository.GetAllOrderItemsAsync();
            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetOrderItem(int id)
        {
            var orderItem = await _orderItemRepository.GetOrderItemByIdAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return Ok(orderItem);
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItemsByOrderId(int orderId)
        {
            var orderItems = await _orderItemRepository.GetOrderItemsByOrderIdAsync(orderId);
            return Ok(orderItems);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin, Moderator")]
        public async Task<ActionResult> AddOrderItem([FromBody] OrderItem orderItem)
        {
            await _orderItemRepository.AddOrderItemAsync(orderItem);
            return CreatedAtAction(nameof(GetOrderItem), new { id = orderItem.Id }, orderItem);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin, Moderator")]
        public async Task<ActionResult> UpdateOrderItem(int id, [FromBody] OrderItem orderItem)
        {
            if (id != orderItem.Id)
            {
                return BadRequest();
            }

            await _orderItemRepository.UpdateOrderItemAsync(orderItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin, Moderator")]
        public async Task<ActionResult> SoftDeleteOrderItem(int id)
        {
            await _orderItemRepository.SoftDeleteOrderItemAsync(id);
            return NoContent();
        }
    }
}

