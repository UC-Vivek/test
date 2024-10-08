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

namespace JumboJuice.Pages.ShoppingCartItems
{
    public class EditModel : PageModel
    {
        private readonly JumboJuice.Data.ApplicationDbContext _context;

        public EditModel(JumboJuice.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ShoppingCartItem ShoppingCartItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingcartitem =  await _context.ShoppingCartItem.FirstOrDefaultAsync(m => m.ShoppingCartItemId == id);
            if (shoppingcartitem == null)
            {
                return NotFound();
            }
            ShoppingCartItem = shoppingcartitem;
           ViewData["ItemId"] = new SelectList(_context.Item, "ItemId", "ItemId");
           ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "ShoppingCartId", "ShoppingCartId");
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

            _context.Attach(ShoppingCartItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartItemExists(ShoppingCartItem.ShoppingCartItemId))
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

        private bool ShoppingCartItemExists(int id)
        {
            return _context.ShoppingCartItem.Any(e => e.ShoppingCartItemId == id);
        }
    }
}
