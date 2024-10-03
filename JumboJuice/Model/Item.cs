namespace GroupProject.Model
{
    public class Item
    {
        public int ItemId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }

        // Navigation properties
        public List<Order>? Orders { get; set; } // Many-to-Many with Order
        public List<ShoppingCart>? Carts { get; set; } // Many-to-Many with Cart
    }
}
