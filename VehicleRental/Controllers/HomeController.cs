using Microsoft.AspNetCore.Mvc;

namespace VehicleRental.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}