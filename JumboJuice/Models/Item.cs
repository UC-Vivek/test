namespace JumboJuice.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Ingredients { get; set; }
        public string? ImagePath { get; set; }
        public int Stock { get; set; }

        // Navigation properties
        public List<OrderItem>? OrderItems { get; set; }  // Each Item can be in multiple OrderItems
        public List<ShoppingCart>? Carts { get; set; } // Many-to-Many with Cart
    }
}
