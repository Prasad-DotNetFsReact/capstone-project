using System.ComponentModel.DataAnnotations;

namespace One8_backend.Models
{
    public enum RestaurantType
    {
        Veg,
        NonVeg,
        Both
    }
    public class Restaurant

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsVeg { get; set; }
        public string ImageUrl { get; set; }

        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<MenuItem>? MenuItems { get; set; } = new List<MenuItem>();
        public virtual ICollection<Review>? Reviews { get; set; } = new List<Review>();
    }
}
