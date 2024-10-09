using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JumboJuice.Models;

namespace JumboJuice.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<JumboJuice.Models.Customer> Customer { get; set; } = default!;
        public DbSet<JumboJuice.Models.Item> Item { get; set; } = default!;
        public DbSet<JumboJuice.Models.OrderItem> OrderItem { get; set; } = default!;
        public DbSet<JumboJuice.Models.Order> Order { get; set; } = default!;
        public DbSet<JumboJuice.Models.ShoppingCart> ShoppingCart { get; set; } = default!;
        public DbSet<JumboJuice.Models.ShoppingCartItem> ShoppingCartItem { get; set; } = default!;
    }
}
