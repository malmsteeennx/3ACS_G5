using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using VehicleRental.Models;
using VehicleRental.Data;

public static class DatabaseSeeder
{
    public static void SeedAdmin(ApplicationDbContext context)
    {
        if (!context.Admins.Any()) // ✅ Ensure no admin exists before inserting
        {
            var admin = new Admin
            {
                Username = "admin",
                PasswordHash = HashPassword("admin123") // ✅ Hash password
            };

            context.Admins.Add(admin);
            context.SaveChanges();
        }
    }

    private static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
