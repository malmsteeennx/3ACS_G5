namespace VehicleRental.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public List<Vehicle> Vehicles { get; set; }
    }
}
