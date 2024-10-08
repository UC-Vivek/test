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
    public class IndexModel : PageModel
    {
        private readonly JumboJuice.Data.ApplicationDbContext _context;

        public IndexModel(JumboJuice.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ShoppingCartItem> ShoppingCartItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ShoppingCartItem = await _context.ShoppingCartItem
                .Include(s => s.Item)
                .Include(s => s.ShoppingCart).ToListAsync();
        }
    }
}
