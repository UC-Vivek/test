using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using JumboJuice.Data;
using JumboJuice.Models;

namespace JumboJuice.Pages
{
    public class MyOrdersModel : PageModel
    {
        private readonly JumboJuice.Data.ApplicationDbContext _context;

        public MyOrdersModel(JumboJuice.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Order> Orders { get; set; } = new List<Order>();

        public async Task OnGetAsync(int customerId)
        {
            // Fetch the customer's orders
            Orders = await _context.Order
                .Where(o => o.CustomerId == 1) // Assuming you have a CustomerId
                .OrderByDescending(o => o.OrderDate) // Orders from latest to oldest
                .ToListAsync();
        }
    }
}
