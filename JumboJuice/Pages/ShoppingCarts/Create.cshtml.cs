using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JumboJuice.Data;
using JumboJuice.Models;

namespace JumboJuice.Pages.ShoppingCarts
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
        ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId");
        ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId");
            return Page();
        }

        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ShoppingCart.Add(ShoppingCart);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
