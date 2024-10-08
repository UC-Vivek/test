using JumboJuice.Models;
using JumboJuice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using JumboJuice.Data;


namespace JumboJuice.Pages
{
    public class ShopModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ShoppingCartService _shoppingCartService;

        public ShopModel(ApplicationDbContext context, ShoppingCartService shoppingCartService)
        {
            _context = context;
            _shoppingCartService = shoppingCartService;
        }

        public IList<Item> Items { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Items = await _context.Item.ToListAsync();
        }

        // Add an item to the shopping cart instead of directly creating an order
        public async Task<IActionResult> OnPostAddToCartAsync(int ItemId, int CustomerId, int Quantity)
        {

            // Add the item to the shopping cart
            await _shoppingCartService.AddItemToCartAsync(CustomerId, ItemId, Quantity);

            // Redirect back to the shop page or to a confirmation page
            return RedirectToPage("/Cart");

        }
    
    }
}

