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
            ViewBag.OwnerName = owner?.Name ?? "Owner"; // ✅ Display owner name in the navbar

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

            int ownerId = HttpContext.Session.GetInt32("OwnerId") ?? 0;
            var owner = _context.Owners.Find(ownerId);

            ViewBag.OwnerId = ownerId;
            ViewBag.OwnerName = owner?.Name ?? "Owner"; // ✅ Ensure Owner Name is passed

            return View();
        }

        // ✅ Add Vehicle (Handles Form Submission)
        [HttpPost]
        public async Task<IActionResult> AddCar(int OwnerId, string Name, string Model, int Year, int SeatCapacity, string FuelType, decimal Price, string Image)
        {
            if (HttpContext.Session.GetString("OwnerLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            if (OwnerId == 0)
            {
                TempData["ErrorMessage"] = "Invalid owner ID. Please log in again.";
                return RedirectToAction("AddCar");
            }

            // ✅ Check if a car with the same details already exists
            bool carExists = await _context.Vehicles.AnyAsync(v =>
                v.Name == Name &&
                v.Model == Model &&
                v.Year == Year &&
                v.FuelType == FuelType &&
                v.SeatCapacity == SeatCapacity &&
                v.OwnerId == OwnerId
            );

            if (carExists)
            {
                TempData["ErrorMessage"] = "This car already exists in your inventory!";
                return RedirectToAction("AddCar");
            }

            var vehicle = new Vehicle
            {
                Name = Name,
                Model = Model,
                Year = Year,
                SeatCapacity = SeatCapacity,
                FuelType = FuelType,
                OwnerId = OwnerId,
                Image = string.IsNullOrEmpty(Image) ? "default-car.png" : Image,
                Price = Price,
                Status = "Available"
            };

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Car added successfully!";
            return RedirectToAction("AddCar");
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

        // ✅ Display Available Vehicles for Rent
        public async Task<IActionResult> Rent()
        {
            var availableVehicles = await _context.Vehicles
                .Include(v => v.Owner) // ✅ Fetch Owner details
                .Where(v => v.Status == "Available")
                .ToListAsync();

            ViewBag.AvailableVehicles = availableVehicles;

            // ✅ Debugging: Print vehicle count to console/log
            Console.WriteLine("Available Vehicles Found: " + availableVehicles.Count);

            return View();
        }
    }
}
