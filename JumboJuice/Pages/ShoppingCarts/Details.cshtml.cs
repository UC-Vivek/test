using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JumboJuice.Data;
using JumboJuice.Models;

namespace JumboJuice.Pages.ShoppingCarts
{
    public class DetailsModel : PageModel
    {
        private readonly JumboJuice.Data.ApplicationDbContext _context;

        public DetailsModel(JumboJuice.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ShoppingCart ShoppingCart { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingcart = await _context.ShoppingCart.FirstOrDefaultAsync(m => m.ShoppingCartId == id);
            if (shoppingcart == null)
            {
                return NotFound();
            }
            else
            {
                ShoppingCart = shoppingcart;
            }
            return Page();
        }
    }
}
