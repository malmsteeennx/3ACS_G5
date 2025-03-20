using Microsoft.AspNetCore.Mvc;
using VehicleRental.Data;

namespace VehicleRental.Controllers
{
    public class Dashboard : Controller
    {
        private readonly ApplicationDbContext _context;

        public Dashboard(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult DeleteRental(int rentalId)
        {
            // Logic to delete the rental from the database
            var rental = _context.Rentals.FirstOrDefault(r => r.Id == rentalId);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
                _context.SaveChanges();
            }

            // Redirect back to the Dashboard or Index page
            return RedirectToAction("Index");
        }
    }
}
