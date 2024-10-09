using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JumboJuice.Data;
using JumboJuice.Models;

namespace JumboJuice.Services
{
    public class ShoppingCartService
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get or create a shopping cart for the customer
        public async Task<ShoppingCart> GetOrCreateCartAsync(int customerId)
        {
            var cart = await _context.ShoppingCart
                .Include(c => c.ShoppingCartItems)
                .ThenInclude(ci => ci.Item)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null)
            {
                // Create a new cart if one doesn't exist
                cart = new ShoppingCart
                {
                    CustomerId = customerId,
                    ShoppingCartItems = new List<ShoppingCartItem>()
                };
                _context.ShoppingCart.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }

        // Add item to the shopping cart
        public async Task AddItemToCartAsync(int customerId, int itemId, int quantity)
        {
            var cart = await GetOrCreateCartAsync(customerId);
            var item = await _context.Item.FindAsync(itemId);

            if (item != null)
            {
                var cartItem = cart.ShoppingCartItems.FirstOrDefault(ci => ci.ItemId == itemId);
                if (cartItem == null)
                {
                    // Add a new item to the cart if it's not already there
                    cartItem = new ShoppingCartItem
                    {
                        ItemId = itemId,
                        ShoppingCartId = cart.ShoppingCartId,
                        Quantity = quantity,
                        Price = item.Price
                    };
                    cart.ShoppingCartItems.Add(cartItem);
                    _context.ShoppingCart.Update(cart);  // Explicitly update the cart
                }
                else
                {
                    // Update the quantity if the item is already in the cart
                    cartItem.Quantity += quantity;
                    _context.ShoppingCartItem.Update(cartItem);  // Update the individual cart item
                }

                await _context.SaveChangesAsync();
            }
        }


        // Convert the shopping cart into an order and return the new order ID
        public async Task<int> ConvertCartToOrderAsync(int customerId)
        {
            var cart = await _context.ShoppingCart
                .Include(c => c.ShoppingCartItems)
                .ThenInclude(ci => ci.Item)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart != null && cart.ShoppingCartItems.Any())
            {
                // Create a new order
                var order = new Order
                {
                    CustomerId = customerId,
                    OrderDate = System.DateTime.Now,
                    TotalAmount = cart.ShoppingCartItems.Sum(ci => ci.Quantity * ci.Price),
                    OrderItems = new List<OrderItem>()
                };

                // Add items from the shopping cart to the order
                foreach (var cartItem in cart.ShoppingCartItems)
                {
                    var orderItem = new OrderItem
                    {
                        ItemId = cartItem.ItemId,
                        Quantity = cartItem.Quantity,
                        Price = cartItem.Price
                    };
                    order.OrderItems.Add(orderItem);
                }

                // Add the new order to the database
                _context.Order.Add(order);
                await _context.SaveChangesAsync();

                // Return the ID of the new order
                return order.OrderId;
            }

            // Handle the case where no items are in the cart
            return -1; // Optionally, you could throw an exception or handle it in another way
        }

        // Clear the shopping cart after checkout
        public async Task ClearCartAsync(int customerId)
        {
            var cart = await _context.ShoppingCart
                .Include(c => c.ShoppingCartItems)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart != null && cart.ShoppingCartItems.Any())
            {
                // Remove all items from the cart
                _context.ShoppingCartItem.RemoveRange(cart.ShoppingCartItems);
                await _context.SaveChangesAsync();
            }
        }

        // Retrieve the items in the cart for the specified customer
        public async Task<List<ShoppingCartItem>> GetCartItemsAsync(int customerId)
        {
            var cart = await _context.ShoppingCart
                .Include(c => c.ShoppingCartItems)
                .ThenInclude(ci => ci.Item)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            return cart?.ShoppingCartItems ?? new List<ShoppingCartItem>();
        }
    }
}