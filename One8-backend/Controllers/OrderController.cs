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
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        //[Authorize(Roles ="Admin")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("user/{userId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserId(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        [HttpPost]
        //[Authorize(Roles = "Customer")]
        public async Task<ActionResult> AddOrder([FromBody] Order order)
        {
            await _orderRepository.AddOrderAsync(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            await _orderRepository.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult> SoftDeleteOrder(int id)
        {
            await _orderRepository.SoftDeleteOrderAsync(id);
            return NoContent();
        }
    }
}

