using Microsoft.AspNetCore.Mvc;
using CarRental.Services;
using CarRental.Models;
using CarRental.Data;

namespace CarRental.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly CarRentalContext _context;

        public HomeController(IVehicleService vehicleService, CarRentalContext context)
        {
            _vehicleService = vehicleService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return View(vehicles);
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View(new ContactMessage());
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactMessage model)
        {
            if (ModelState.IsValid)
            {
                model.SentAt = DateTime.Now; // Save current time
                _context.ContactMessages.Add(model);
                _context.SaveChanges();

                TempData["ContactSuccess"] = "✅ Your message has been sent successfully!";
                return RedirectToAction("Contact");
            }

            return View(model);
        }
    }
} 