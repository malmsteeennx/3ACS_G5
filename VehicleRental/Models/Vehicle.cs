using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRental.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "Unknown";

        public string CarType { get; set; }

        public string Model { get; set; } = "Not specified";

        public int Year { get; set; } = 2000;

        public int SeatCapacity { get; set; } = 4;

        public string FuelType { get; set; } = "Unknown";

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string Image { get; set; } = "default-car.png";

        [Required]
        public string Status { get; set; } = "Available";

        [Required]
        public int OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DatePosted { get; set; } = DateTime.Now;

        // ✅ NEW: Available Days (Comma-separated string)
        public string AvailableDays { get; set; } = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday";
    }
}
