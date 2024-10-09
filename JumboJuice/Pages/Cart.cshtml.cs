using JumboJuice.Models;
using JumboJuice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JumboJuice.Pages
{
    public class CartModel : PageModel
    {
        private readonly ShoppingCartService _shoppingCartService;

        public CartModel(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public List<ShoppingCartItem> CartItems { get; set; } = new List<ShoppingCartItem>();

        public async Task OnGetAsync()
        {
            // Hardcoded customer ID for testing; replace with actual CustomerId from authentication
            int customerId = 1;

            // Retrieve the current cart items
            CartItems = await _shoppingCartService.GetCartItemsAsync(customerId);
        }

        // This method handles the checkout process
        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            int customerId = 1; // Replace with actual CustomerId from authentication

            // Step 1: Convert the shopping cart to an order
            var orderId = await _shoppingCartService.ConvertCartToOrderAsync(customerId);

            // Step 2: Clear the shopping cart
            await _shoppingCartService.ClearCartAsync(customerId);

            // Step 3: Redirect to the order confirmation page, passing the order ID
            return RedirectToPage("/OrderConfirmation", new { orderId = orderId });
        }
    }
}
