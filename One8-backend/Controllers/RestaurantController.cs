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
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            var restaurants = await _restaurantRepository.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _restaurantRepository.GetRestaurantByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpPost]

        //[Authorize (Roles ="Admin")]
        public async Task<ActionResult> AddRestaurant([FromBody] Restaurant restaurant)
        {
            await _restaurantRepository.AddRestaurantAsync(restaurant);
            return CreatedAtAction(nameof(GetRestaurant), new { id = restaurant.Id }, restaurant);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin,Moderator")]

        public async Task<ActionResult> UpdateRestaurant(int id, [FromBody] Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            await _restaurantRepository.UpdateRestaurantAsync(restaurant);
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> SoftDeleteRestaurant(int id)
        {
            await _restaurantRepository.SoftDeleteRestaurantAsync(id);
            return NoContent();
        }
    }
}

