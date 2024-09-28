namespace One8_backend.Models
{

    public class Review
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

        public bool IsDeleted { get; set; } = false;

        public Restaurant? Restaurant { get; set; }
        public User? User { get; set; }

    }
}

