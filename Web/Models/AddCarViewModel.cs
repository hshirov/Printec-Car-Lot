using Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class AddCarViewModel
    {
        [Display(Name = "First Name")]
        [Required]
        public string OwnerFirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string OwnerLastName { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public Color Color { get; set; }
        [Display(Name = "License Plate")]
        [Required]
        public string LicensePlate { get; set; }
        [Display(Name = "Engine Capacity(cc)")]
        [Required]
        public int EngineCapacityInCC { get; set; }
        [Display(Name = "Horse Power")]
        [Required]
        public int HorsePower { get; set; }
    }
}
