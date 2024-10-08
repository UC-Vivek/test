using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JumboJuice.Data;
using JumboJuice.Models;

namespace JumboJuice.Pages.OrderItems
{
    public class DetailsModel : PageModel
    {
        private readonly JumboJuice.Data.ApplicationDbContext _context;

        public DetailsModel(JumboJuice.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public OrderItem OrderItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderitem = await _context.OrderItem.FirstOrDefaultAsync(m => m.OrderItemId == id);
            if (orderitem == null)
            {
                return NotFound();
            }
            else
            {
                OrderItem = orderitem;
            }
            return Page();
        }
    }
}
