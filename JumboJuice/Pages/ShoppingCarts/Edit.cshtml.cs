using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JumboJuice.Data;
using JumboJuice.Models;

namespace JumboJuice.Pages.ShoppingCarts
{
    public class EditModel : PageModel
    {
        private readonly JumboJuice.Data.ApplicationDbContext _context;

        public EditModel(JumboJuice.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingcart =  await _context.ShoppingCart.FirstOrDefaultAsync(m => m.ShoppingCartId == id);
            if (shoppingcart == null)
            {
                return NotFound();
            }
            ShoppingCart = shoppingcart;
           ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId");
           ViewData["OrderId"] = new SelectList(_context.Order, "OrderId", "OrderId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ShoppingCart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartExists(ShoppingCart.ShoppingCartId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ShoppingCartExists(int id)
        {
            return _context.ShoppingCart.Any(e => e.ShoppingCartId == id);
        }
    }
}
