using Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class EditCarViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        [Display(Name = "First Name")]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "Name must contain letters only.")]
        [MinLength(2, ErrorMessage = "The minimum length is 2")]
        [Required]
        public string OwnerFirstName { get; set; }
        [Display(Name = "Last Name")]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "Name must contain letters only.")]
        [MinLength(2, ErrorMessage = "The minimum length is 2")]
        [Required]
        public string OwnerLastName { get; set; }
        [Required]
        public Color Color { get; set; }
        [Display(Name = "License Plate")]
        [MinLength(4, ErrorMessage = "The minimum length is 4")]
        [Required]
        public string LicensePlate { get; set; }
        [Display(Name = "Engine Capacity(cc)")]
        [Range(0, 10000, ErrorMessage = "Invalid value.")]
        [Required]
        public int EngineCapacityInCC { get; set; }
        [Display(Name = "Horse Power")]
        [Range(1, 10000, ErrorMessage = "Invalid value.")]
        [Required]
        public int HorsePower { get; set; }
    }
}
