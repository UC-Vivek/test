namespace GroupProject.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        // Navigation properties
        public List<Order>? Orders { get; set; } // One-to-Many with Order
        public ShoppingCart? Carts { get; set; } // One-to-One with Cart
    }
}
