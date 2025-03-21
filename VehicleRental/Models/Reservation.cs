using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRental.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VehicleId { get; set; }

        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; }

        [Required]
        public string Name { get; set; } // User's Name

        [Required]
        public string ContactInfo { get; set; } // Phone/Email

        [Required]
        public string PickupLocation { get; set; }

        [Required]
        public string DropoffLocation { get; set; }

        [Required]
        public DateTime PickupDate { get; set; }

        [Required]
        public DateTime DropoffDate { get; set; }

        [Required]
        public string BookingId { get; set; } // Unique identifier for reservation

        [Required]
        public string PinCode { get; set; } // Code to manage reservation

        [Required]
        public string Status { get; set; } = "Reserved"; // "Reserved" or "Cancelled"

        [Required]
        public string PaymentStatus { get; set; } = "Awaiting Fulfillment"; // "Paid" or "Awaiting Fulfillment"

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
