namespace VehicleRental.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string RenterName { get; set; }
        public DateTime RentDate { get; set; } = DateTime.Now;
        public DateTime? ReturnDate { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
