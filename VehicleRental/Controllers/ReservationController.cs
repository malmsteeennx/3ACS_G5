using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRental.Data;
using VehicleRental.Models;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Show Reservation Page (Search Cars)
        public IActionResult Index()
        {
            return View("Reservation"); // Loads Views/Reservation/Reservation.cshtml
        }

        // ✅ Show Booking Form
        public async Task<IActionResult> Book(int vehicleId)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);
            if (vehicle == null) return NotFound();

            ViewBag.Vehicle = vehicle;
            return View("Booking"); // Matches Views/Reservation/Booking.cshtml
        }

        // ✅ Manage Reservation (Enter Booking ID & PIN)
        public IActionResult Manage()
        {
            return View("Manage"); // Matches Views/Reservation/Manage.cshtml
        }

        // ✅ Show Booking Success Page
        public IActionResult BookingSuccess()
        {
            return View("BookingSuccess"); // Matches Views/Reservation/BookingSuccess.cshtml
        }
    }
}
