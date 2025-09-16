using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        
        [Required]
        public int VehicleID { get; set; }
        
        public int? CustomerID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime PickupDate { get; set; }
        
        [Required]
        [Range(1, 30)]
        public int RentalDays { get; set; }
        
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; 
        
        public DateTime BookingDate { get; set; } = DateTime.Now;
        
        public decimal TotalAmount { get; set; }
        
        
        public virtual Vehicle Vehicle { get; set; } = null!;
        public virtual Customer? Customer { get; set; }
        //git Gajan pull checking 
        
    }
} 
