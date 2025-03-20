using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRental.Data;
using VehicleRental.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cars()
        {
            return View();
        }

        public IActionResult Reservation()
        {
            return View();
        }

        // ✅ Display Available Vehicles for Rent
        public async Task<IActionResult> Rent()
        {
            var availableVehicles = await _context.Vehicles
                .Include(v => v.Owner) // ✅ Include Owner details
                .Where(v => v.Status == "Available")
                .Select(v => new Vehicle
                {
                    Id = v.Id,
                    Name = v.Name ?? "Unknown",  // ✅ Handle NULL values
                    Model = v.Model ?? "Not specified",  // ✅ Handle NULL values
                    Year = v.Year != 0 ? v.Year : 2000,  // ✅ Set a default value if NULL
                    SeatCapacity = v.SeatCapacity != 0 ? v.SeatCapacity : 4, // ✅ Default seat capacity
                    FuelType = v.FuelType ?? "Unknown", // ✅ Handle NULL values
                    Price = v.Price,
                    Image = string.IsNullOrEmpty(v.Image) ? "default-car.png" : v.Image,  // ✅ Provide a default image
                    Owner = v.Owner ?? new Owner { Name = "Unknown Owner" } // ✅ Handle NULL Owner
                })
                .ToListAsync();

            // Debugging Output
            Debug.WriteLine($"Available Vehicles Found: {availableVehicles.Count}");

            ViewBag.AvailableVehicles = availableVehicles;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
