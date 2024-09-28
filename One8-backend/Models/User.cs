using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace One8_backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }
    }
}
