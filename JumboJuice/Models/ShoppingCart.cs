namespace JumboJuice.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }

        // Foreign keys
        public int CustomerId { get; set; }
        public Customer? Customers { get; set; } // One-to-One with Customer

        // Navigation property: Cart can have multiple items
        public List<ShoppingCartItem>? ShoppingCartItems { get; set; }

        public int OrderId { get; set; }
        public Order? Orders { get; set; } // One-to-One with Order

    }
}
