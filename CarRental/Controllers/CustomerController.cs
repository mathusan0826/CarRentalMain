using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;
using System.Security.Cryptography;
using System.Text;

namespace CarRental.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CarRentalContext _context;

        public CustomerController(CarRentalContext context)
        {
            _context = context;
        }

        // GET: Customer/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Customer/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                // Check if username already exists
                if (await _context.Customers.AnyAsync(c => c.Username == customer.Username))
                {
                    ModelState.AddModelError("Username", "Username already exists");
                    return View(customer);
                }

                // Check if email already exists
                if (await _context.Customers.AnyAsync(c => c.Email == customer.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    return View(customer);
                }

                // Hash password
                customer.PasswordHash = HashPassword(customer.PasswordHash);
                customer.Role = UserRole.Customer;
                customer.CreatedDate = DateTime.Now;
                customer.IsActive = true;

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                // Set session
                HttpContext.Session.SetString("CustomerUsername", customer.Username);
                HttpContext.Session.SetString("CustomerRole", customer.Role.ToString());
                HttpContext.Session.SetInt32("CustomerID", customer.CustomerID);

                return RedirectToAction("Index", "Home");
            }

            return View(customer);
        }

        // GET: Customer/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Customer/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Username and password are required");
                return View();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Username == username && c.IsActive);

            if (customer != null && VerifyPassword(password, customer.PasswordHash))
            {
                // Set session
                HttpContext.Session.SetString("CustomerUsername", customer.Username);
                HttpContext.Session.SetString("CustomerRole", customer.Role.ToString());
                HttpContext.Session.SetInt32("CustomerID", customer.CustomerID);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        // GET: Customer/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // GET: Customer/Profile
        public async Task<IActionResult> Profile()
        {
            var customerId = HttpContext.Session.GetInt32("CustomerID");
            if (customerId == null)
            {
                return RedirectToAction("Login");
            }

            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
            {
                return RedirectToAction("Login");
            }

            return View(customer);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}


