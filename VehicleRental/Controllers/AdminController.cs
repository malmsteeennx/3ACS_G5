using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRental.Data;
using VehicleRental.Models;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ ADMIN DASHBOARD - Displays all vehicles and owners
        public async Task<IActionResult> Index()
        {
            ViewBag.AllVehicles = await _context.Vehicles
                .Include(v => v.Owner)
                .ToListAsync();

            ViewBag.AllOwners = await _context.Owners.ToListAsync();

            return View();
        }

        // ✅ CAR APPROVALS - Displays pending vehicle approvals
        public async Task<IActionResult> CarApprovals()
        {
            ViewBag.PendingVehicles = await _context.Vehicles
                .Include(v => v.Owner)
                .Where(v => v.Status == "Pending Approval")
                .ToListAsync();

            return View();
        }

        // ✅ APPROVE VEHICLE - Makes the vehicle available for renting
        public async Task<IActionResult> ApproveVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                vehicle.Status = "Available"; // ✅ Approved, now visible in Rent page
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("CarApprovals");
        }

        // ✅ REJECT VEHICLE - Keeps it in database but changes status to "Rejected"
        public async Task<IActionResult> RejectVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                vehicle.Status = "Rejected"; // ✅ Mark as rejected instead of deleting
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("CarApprovals");
        }

        // ✅ EDIT VEHICLE (Placeholder)
        public IActionResult EditVehicle(int id)
        {
            return View();
        }

        // ✅ DELETE VEHICLE - Removes vehicle from database
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // ✅ DELETE OWNER - Removes owner and all related vehicles
        public async Task<IActionResult> DeleteOwner(int id)
        {
            var owner = await _context.Owners.Include(o => o.Vehicles).FirstOrDefaultAsync(o => o.Id == id);
            if (owner != null)
            {
                _context.Vehicles.RemoveRange(owner.Vehicles);
                _context.Owners.Remove(owner);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // ✅ LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home"); // Redirect to home after logout
        }
    }
}
