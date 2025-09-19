// Models/Payment.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }

        public int BookingID { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // "Pending", "Completed", "Failed"

        // Navigation property to link to the Booking
        public virtual Booking Booking { get; set; } = null!;
    }
}