namespace One8_backend.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }


        public int MenuItemId { get; set; }

        public bool IsDeleted { get; set; } = false;
        public virtual MenuItem? MenuItem { get; set; }


        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }
}
