using Microsoft.AspNetCore.Mvc;
using CarRental.Services;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IVehicleService _vehicleService;

        public BookingController(IBookingService bookingService, IVehicleService vehicleService)
        {
            _bookingService = bookingService;
            _vehicleService = vehicleService;
        }

        [HttpGet("Booking/Book/{vehicleId:int}")]
        public async Task<IActionResult> Book(int vehicleId)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
            {
                return NotFound();
            }

            var booking = new Booking
            {
                VehicleID = vehicleId,
                PickupDate = DateTime.Today.AddDays(1)
            };

            ViewBag.Vehicle = vehicle;
            ViewBag.IsCustomerLoggedIn = HttpContext.Session.GetString("CustomerRole") == "Customer";
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(Booking booking)
        {
            // Guard: require customer login to place a booking
            var isCustomerLoggedIn = HttpContext.Session.GetString("CustomerRole") == "Customer";
            if (!isCustomerLoggedIn)
            {
                TempData["AuthRequired"] = "Please login if you are already registered, or register to continue booking.";
                return RedirectToAction("Login", "Customer", new { returnUrl = Url.Action("Book", new { vehicleId = booking.VehicleID }) });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bookingService.CreateBookingAsync(booking);
                    return RedirectToAction(nameof(Success), new { bookingId = booking.BookingID });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "An error occurred while creating the booking. Please try again.");
                }
            }

            var vehicle = await _vehicleService.GetVehicleByIdAsync(booking.VehicleID);
            ViewBag.Vehicle = vehicle;
            ViewBag.IsCustomerLoggedIn = HttpContext.Session.GetString("CustomerRole") == "Customer";
            return View(booking);
        }

        public async Task<IActionResult> Success(int bookingId)
        {
            var booking = await _bookingService.GetBookingByIdAsync(bookingId);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> CalculateTotal(int vehicleId, int rentalDays)
        {
            var totalAmount = await _bookingService.CalculateTotalAmountAsync(vehicleId, rentalDays);
            return Json(new { totalAmount });
        }
    }
} 