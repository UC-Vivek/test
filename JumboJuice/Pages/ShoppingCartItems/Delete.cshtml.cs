using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JumboJuice.Data;
using JumboJuice.Models;

namespace JumboJuice.Pages.ShoppingCartItems
{
    public class DeleteModel : PageModel
    {
        private readonly JumboJuice.Data.ApplicationDbContext _context;

        public DeleteModel(JumboJuice.Data.ApplicationDbContext context)
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

            var shoppingcartitem = await _context.ShoppingCartItem.FirstOrDefaultAsync(m => m.ShoppingCartItemId == id);

            if (shoppingcartitem == null)
            {
                return NotFound();
            }
            else
            {
                ShoppingCartItem = shoppingcartitem;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingcartitem = await _context.ShoppingCartItem.FindAsync(id);
            if (shoppingcartitem != null)
            {
                ShoppingCartItem = shoppingcartitem;
                _context.ShoppingCartItem.Remove(ShoppingCartItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
