using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VehicleRental.Data;
using VehicleRental.Models;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRental.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Show Signup Page
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        // ✅ Process Signup
        [HttpPost]
        public async Task<IActionResult> Signup(string name, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ViewBag.Message = "Passwords do not match!";
                return View();
            }

            if (await _context.Owners.AnyAsync(o => o.Email == email))
            {
                ViewBag.Message = "Email already exists!";
                return View();
            }

            var hashedPassword = HashPassword(password);
            var owner = new Owner { Name = name, Email = email, PasswordHash = hashedPassword };

            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("OwnerLoggedIn", "true");
            HttpContext.Session.SetInt32("OwnerId", owner.Id);

            return RedirectToAction("Dashboard", "Owner");
        }

        // ✅ Show Login Page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // ✅ Process Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var owner = await _context.Owners.FirstOrDefaultAsync(o => o.Email == email);
            if (owner == null || !VerifyPassword(password, owner.PasswordHash))
            {
                ViewBag.Message = "Invalid email or password!";
                return View();
            }

            HttpContext.Session.SetString("OwnerLoggedIn", "true");
            HttpContext.Session.SetInt32("OwnerId", owner.Id);

            return RedirectToAction("Dashboard", "Owner");
        }

        // ✅ Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // 🔒 Secure Password Hashing
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        // 🔒 Verify Hashed Password
        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }
    }
}
