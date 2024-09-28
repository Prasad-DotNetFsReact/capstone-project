using System.ComponentModel.DataAnnotations;

namespace One8_backend.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public  string MenuUrl { get; set; }

        public bool IsDeleted { get; set; } = false;

        public Restaurant? Restaurant { get; set; }
    }
}