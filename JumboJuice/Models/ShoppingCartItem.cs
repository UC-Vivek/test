namespace JumboJuice.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }

        public int ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }  // Link to the ShoppingCart

        public int ItemId { get; set; }
        public Item? Item { get; set; }  // Link to the Item

        public int Quantity { get; set; }
        public decimal Price { get; set; }  // Price at the time the item was added to the cart
    }
}
