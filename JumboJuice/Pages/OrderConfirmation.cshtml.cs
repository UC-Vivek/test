using JumboJuice.Models;
using JumboJuice.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JumboJuice.Pages
{
    public class OrderConfirmationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public OrderConfirmationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Order Order { get; set; } = new Order();

        public async Task OnGetAsync(int orderId)
        {
            // Fetch the order details using the order ID
            Order = await _context.Order
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Item)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }
    }
}
