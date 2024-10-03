namespace GroupProject.Model
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }

        // Foreign keys
        public int CustomerId { get; set; }
        public Customer? Customers { get; set; } // One-to-One with Customer

        public int OrderId { get; set; }
        public Order? Orders { get; set; } // One-to-One with Order

        // Navigation properties
        public List<Item>? Items { get; set; } // Many-to-Many with Item
    }
}
