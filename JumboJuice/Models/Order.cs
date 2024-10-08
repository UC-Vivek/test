namespace JumboJuice.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Foreign key
        public int CustomerId { get; set; }
        public Customer? Customers { get; set; } // Many-to-One with Customer
                                                 // Navigation properties
        public List<OrderItem>? OrderItems { get; set; }  // Each Order has multiple OrderItems
        public ShoppingCart? Carts { get; set; } // One-to-One with Cart
    }
}
