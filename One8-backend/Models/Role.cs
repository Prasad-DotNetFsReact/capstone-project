namespace One8_backend.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        public virtual ICollection<User>? Users { get; set; }
    }
}
