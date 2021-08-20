using Data.Enums;

namespace Web.Models
{
    public class CarIndexViewModel
    {
        public int Id { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public Color Color { get; set; }
        public string LicensePlate { get; set; }
        public int EngineCapacityInCC { get; set; }
        public int HorsePower { get; set; }
    }
}