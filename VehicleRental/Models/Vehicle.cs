using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRental.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "Unknown";  // ✅ Default value

        public string Model { get; set; } = "Not specified";  // ✅ Default value

        public int Year { get; set; } = 2000;  // ✅ Default value

        public int SeatCapacity { get; set; } = 4; // ✅ Default value

        public string FuelType { get; set; } = "Unknown"; // ✅ Default value

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string Image { get; set; } = "default-car.png"; // ✅ Default value

        [Required]
        public string Status { get; set; } = "Available"; // ✅ Default value

        [Required]
        public int OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DatePosted { get; set; } = DateTime.Now;
    }
}
