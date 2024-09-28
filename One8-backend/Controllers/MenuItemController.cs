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
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemController(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItems()
        {
            var menuItems = await _menuItemRepository.GetAllMenuItemsAsync();
            return Ok(menuItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> GetMenuItem(int id)
        {
            var menuItem = await _menuItemRepository.GetMenuItemByIdAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return Ok(menuItem);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> AddMenuItem([FromBody] MenuItem menuItem)
        {
            await _menuItemRepository.AddMenuItemAsync(menuItem);
            return CreatedAtAction(nameof(GetMenuItem), new { id = menuItem.Id }, menuItem);
            
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin,Moderator")]

        public async Task<ActionResult> UpdateMenuItem(int id, [FromBody] MenuItem menuItem)
        {
            if (id != menuItem.Id)
            {
                return BadRequest();
            }

            await _menuItemRepository.UpdateMenuItemAsync(menuItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> SoftDeleteMenuItem(int id)
        {
            await _menuItemRepository.SoftDeleteMenuItemAsync(id);
            return NoContent();
        }
    }
}

