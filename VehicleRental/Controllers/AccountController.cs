using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VehicleRental.Data;
using VehicleRental.Models;
using VehicleRental.Services;
using System.Threading.Tasks;

namespace VehicleRental.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;

        public AccountController(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // ✅ Show Signup Page
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        // ✅ Process Signup with Email Verification
        [HttpPost]
        public async Task<IActionResult> Signup(string name, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                TempData["ErrorMessage"] = "Passwords do not match!";
                return View();
            }

            if (await _context.Owners.AnyAsync(o => o.Email == email))
            {
                TempData["ErrorMessage"] = "Email already exists!";
                return View();
            }

            var hashedPassword = EmailService.HashPassword(password);
            var verificationToken = System.Guid.NewGuid().ToString(); // Generate a unique token

            var owner = new Owner
            {
                Name = name,
                Email = email,
                PasswordHash = hashedPassword,
                IsVerified = false,
                VerificationToken = verificationToken
            };

            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();

            string domain = "https://e1f4-136-158-33-136.ngrok-free.app"; // Replace with your actual ngrok URL
            string verificationLink = $"{domain}/Account/VerifyEmail?token={verificationToken}";

            // ✅ Send Verification Email
            string emailBody = $"<h2>Welcome to Vehicle Rental</h2><p>Please verify your email by clicking <a href='{verificationLink}'>here</a>.</p>";

            await _emailService.SendEmailAsync(owner.Email, "Verify Your Email", emailBody);

            TempData["SuccessMessage"] = "Signup successful! Please check your email to verify your account.";
            return RedirectToAction("Login");
        }

        // ✅ Email Verification Method
        public async Task<IActionResult> VerifyEmail(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Invalid verification link.");
            }

            var owner = await _context.Owners.FirstOrDefaultAsync(o => o.VerificationToken == token);
            if (owner == null)
            {
                return BadRequest("Invalid or expired verification token.");
            }

            owner.IsVerified = true;
            owner.VerificationToken = null;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Email verified successfully! You can now log in.";
            return RedirectToAction("Login");
        }

        // ✅ Show Login Page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // ✅ Process Login (Only Allow Verified Users)
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var owner = await _context.Owners.FirstOrDefaultAsync(o => o.Email == email);

            if (owner == null || !EmailService.VerifyPassword(password, owner.PasswordHash))
            {
                TempData["ErrorMessage"] = "Invalid email or password!";
                return View();
            }

            if (!owner.IsVerified)
            {
                TempData["ErrorMessage"] = "Please verify your email before logging in.";
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
    }
}
