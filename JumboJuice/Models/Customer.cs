namespace JumboJuice.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; } // Contact number
        public string? Address { get; set; }

        // Navigation properties
        public List<Order>? Orders { get; set; } // One-to-Many with Order
        public ShoppingCart? Carts { get; set; } // One-to-One with Cart
    }
}
