using Data.Enums;

namespace Data.Models
{
    public class Car
    {
        public int Id { get; set; }
        public Owner Owner { get; set; }
        public Model Model { get; set; }
        public Color Color { get; set; }
        public string LicensePlate { get; set; }
        public int EngineCapacityInCC { get; set; }
        public int HorsePower { get; set; }
        public bool IsArchived { get; set; }
    }
}
