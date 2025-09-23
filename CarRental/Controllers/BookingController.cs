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

        [HttpGet("Booking")]
        public async Task<IActionResult> Index()
        {
            var customerId = HttpContext.Session.GetInt32("CustomerID");
            if (customerId == null)
            {
                return RedirectToAction("Login", "Customer", new { returnUrl = Url.Action("Index", "Booking") });
            }

            var allBookings = await _bookingService.GetAllBookingsAsync();
            var myBookings = allBookings
                .Where(b => b.CustomerID == customerId)
                .OrderByDescending(b => b.BookingDate)
                .ToList();

            return View(myBookings);
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

            // Attach logged-in customer to the booking
            var customerId = HttpContext.Session.GetInt32("CustomerID");
            if (customerId.HasValue)
            {
                booking.CustomerID = customerId.Value;
            }

            //Ensure VehicleID is set New code jana
            if (booking.VehicleID == 0)
            {
                //ModelState.AddModelError("VehicleID", "Vehicle selection is required.");
                if (RouteData.Values.ContainsKey("id"))
                {
                    int.TryParse(RouteData.Values["id"].ToString(), out int vehicleId);
                    booking.VehicleID = vehicleId;
                }

            }
            //Clear any VehicleID validation errors since we handle it manually
            if (ModelState.ContainsKey("VehicleID")) ModelState.Remove("VehicleID");
            if (ModelState.ContainsKey("Vehicle")) ModelState.Remove("Vehicle");
            if (ModelState.ContainsKey("Customer")) ModelState.Remove("Customer");


            TempData["DebugInfo"] = $"VehicleID: {booking.VehicleID}, CustomerName: {booking.CustomerName}, ModelState IsValid:{ModelState.IsValid}";
            if (ModelState.IsValid) //bug changed if (!ModelState.IsValid)
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
            else
            {
                //Debug : Show validation errors
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["ValidationErrors"] = string.Join("; ", errors);

            }
            // Re-render form with errors (new code jan)
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