using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using One8_backend.Models;
using One8_backend.Data;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace One8_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly One8DbContext _context;
        private readonly IConfiguration _config;

        public LoginController(One8DbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        public IActionResult Login([FromBody] User loginUser)
        {
            if (loginUser == null || string.IsNullOrEmpty(loginUser.Username) || string.IsNullOrEmpty(loginUser.Password))
            {
                return BadRequest("Invalid username or password.");
            }

            var authenticatedUser = Authenticate(loginUser);
            if (authenticatedUser != null)
            {
                var tokenString = GenerateJSONWebToken(authenticatedUser);
                return Ok(new { token = tokenString });
            }
            return Unauthorized("Username or password is incorrect.");
        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role?.RoleName ?? string.Empty)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(User login)
        {
            var user = _context.Users
                .Include(u => u.Role) // Include Role data
                .SingleOrDefault(u => u.Username == login.Username && u.Password == login.Password);

            return user;
        }
    }
}