using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRental.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        [Column(TypeName = "decimal(18,2)")]  // ✅ Explicitly set decimal precision
        public decimal Price { get; set; }

        public string Status { get; set; } = "Available";

        public Owner Owner { get; set; }
    }
}
