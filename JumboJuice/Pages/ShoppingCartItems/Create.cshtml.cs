using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JumboJuice.Data;
using JumboJuice.Models;

namespace JumboJuice.Pages.ShoppingCartItems
{
    public class CreateModel : PageModel
    {
        private readonly JumboJuice.Data.ApplicationDbContext _context;

        public CreateModel(JumboJuice.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ItemId"] = new SelectList(_context.Item, "ItemId", "ItemId");
        ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "ShoppingCartId", "ShoppingCartId");
            return Page();
        }

        [BindProperty]
        public ShoppingCartItem ShoppingCartItem { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ShoppingCartItem.Add(ShoppingCartItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
