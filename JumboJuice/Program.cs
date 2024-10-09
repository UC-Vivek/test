using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using JumboJuice.Data;
using JumboJuice.Services;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));

// Add Identity services
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>(); // This tells Identity to use your DbContext

// Add Razor Pages support
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));

// Register the ShoppingCartService here
builder.Services.AddTransient<ShoppingCartService>();  // <-- Add this line to register the service

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseStaticFiles();

app.UseAuthorization();

app.MapRazorPages();

app.Run();