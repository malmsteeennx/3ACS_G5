using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VehicleRental.Data;
using VehicleRental.Models;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.Controllers
{
    public class OwnerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OwnerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Show Dashboard (Only for Logged-in Owners)
        public async Task<IActionResult> Dashboard()
        {
            if (HttpContext.Session.GetString("OwnerLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            int ownerId = HttpContext.Session.GetInt32("OwnerId") ?? 0;

            // ✅ Fetch owner details
            var owner = await _context.Owners.FindAsync(ownerId);
            ViewBag.OwnerName = owner?.Name ?? "Owner";

            // ✅ Fetch rented cars
            var rentedCars = await _context.Rentals
                .Include(r => r.Vehicle)
                .Where(r => r.Vehicle.OwnerId == ownerId)
                .ToListAsync();

            ViewBag.RentedCars = rentedCars;

            return View();
        }

        // ✅ Show Add Car Page
        public IActionResult AddCar()
        {
            if (HttpContext.Session.GetString("OwnerLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // ✅ Add Vehicle (Handles Form Submission)
        [HttpPost]
        public async Task<IActionResult> AddCar(string name, decimal price, string image)
        {
            if (HttpContext.Session.GetString("OwnerLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            int ownerId = HttpContext.Session.GetInt32("OwnerId") ?? 0;

            var vehicle = new Vehicle
            {
                Name = name,
                Price = price,
                OwnerId = ownerId,
                Image = string.IsNullOrEmpty(image) ? "default-car.png" : image,
                Status = "Available"
            };

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return RedirectToAction("Dashboard");
        }

        // ✅ Remove Vehicle
        [HttpPost]
        public async Task<IActionResult> RemoveVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Dashboard", "Owner");
        }
    }
}
