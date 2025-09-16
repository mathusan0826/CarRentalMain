using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        public UserRole Role { get; set; } = UserRole.Customer;

        // Navigation
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}



