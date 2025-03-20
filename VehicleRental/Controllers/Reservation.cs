using Microsoft.AspNetCore.Mvc;

namespace VehicleRental.Controllers
{
    public class Reservation : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
