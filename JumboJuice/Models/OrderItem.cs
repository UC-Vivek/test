﻿namespace JumboJuice.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int ItemId { get; set; }
        public Item? Item { get; set; }

        public int Quantity { get; set; } // Add Quantity to Order
        public decimal Price { get; set; }
    }
}
