namespace One8_backend.Models
{

    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; }
        public double TotalPrice { get; set; }
        public int UserId { get; set; }

        public bool IsDeleted { get; set; } = false;
        public virtual User User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

}