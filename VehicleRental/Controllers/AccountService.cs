using System.Security.Cryptography;
using System.Text;

namespace VehicleRental.Services
{
    public class AccountService
    {
        // ✅ Secure Password Hashing
        public string HashPassword(string password)
        {
            using var hmac = new HMACSHA256();
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        // ✅ Verify Hashed Password
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }
    }
}
