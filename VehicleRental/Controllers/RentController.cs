using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRental.Data;
using VehicleRental.Models;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.Controllers
{
    public class RentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Make sure to include the Owner navigation property
            var availableVehicles = await _context.Vehicles
                .Include(v => v.Owner)
                .Where(v => v.Status == "Available")
                .ToListAsync();

            // Debug information
            Console.WriteLine($"Available Vehicles Count: {availableVehicles.Count}");
            foreach (var vehicle in availableVehicles)
            {
                Console.WriteLine($"Vehicle: {vehicle.Name}, Owner: {vehicle?.Owner?.Name ?? "Unknown"}");
            }

            ViewBag.AvailableVehicles = availableVehicles;

            return View("Rent");
        }

        [HttpPost]
        public async Task<IActionResult> RentVehicle(int vehicleId, int userId)
        {
            // Implement the rental logic here
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);

            if (vehicle != null && vehicle.Status == "Available")
            {
                // Update vehicle status
                vehicle.Status = "Rented";

                // Create rental record
                var rental = new Rental
                {
                    VehicleId = vehicleId,
                    UserId = userId,
                    RentalDate = DateTime.Now,
                    ReturnDate = DateTime.Now.AddDays(1) // Default to 1 day rental
                };

                _context.Rentals.Add(rental);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Vehicle rented successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Vehicle is not available for rent.";
            }

            return RedirectToAction("Index");
        }
    }
}

