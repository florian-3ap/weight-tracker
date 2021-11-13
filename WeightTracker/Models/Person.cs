using System.ComponentModel.DataAnnotations;

namespace WeightTracker.Models
{
    public class Person
    {
        [Key] public int Id { get; set; }

        [Display(Name = "Vorname")]
        [Required(ErrorMessage = "Vorname ist erforderlich!")]
        [StringLength(50, ErrorMessage = "Max. ist 50 Zeichen.")]
        public string FirstName { get; set; }

        [Display(Name = "Nachname")]
        [Required(ErrorMessage = "Nachname ist erforderlich!")]
        [StringLength(50, ErrorMessage = "Max. ist 50 Zeichen.")]
        public string LastName { get; set; }

        [Display(Name = "Alter")]
        [Required(ErrorMessage = "Alter ist erforderlich!")]
        [Range(1, 125, ErrorMessage = "Alter zwischen 1 und 125 Jahren")]
        public int Age { get; set; }
    }
}