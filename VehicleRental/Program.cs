using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VehicleRental.Data;
using VehicleRental.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 🔹 Add necessary services
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddSession();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// 🔹 Middleware setup
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
