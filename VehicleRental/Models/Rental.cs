using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRental.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VehicleId { get; set; }

        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public string Status { get; set; } = "Active"; // Active, Completed, Cancelled
    }
}

