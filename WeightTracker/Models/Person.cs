using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeightTracker.Models
{
    public class Person
    {
        [Key] public int Id { get; set; }

        [Display(Name = "Nachname")]
        [Required(ErrorMessage = "Nachname ist erforderlich!")]
        [StringLength(50, ErrorMessage = "Max. ist 50 Zeichen.")]
        public string Name { get; set; }

        [Display(Name = "Vorname")]
        [Required(ErrorMessage = "Vorname ist erforderlich!")]
        [StringLength(50, ErrorMessage = "Max. ist 50 Zeichen.")]
        public string SecondName { get; set; }

        [Display(Name = "Strasse / Nr.")]
        [Required(ErrorMessage = "Strasse / Nr. ist erforderlich!")]
        [StringLength(50, ErrorMessage = "Max. ist 50 Zeichen.")]
        public string Street { get; set; }

        [Display(Name = "PLZ")]
        [Required(ErrorMessage = "PLZ ist erforderlich!")]
        [StringLength(50, ErrorMessage = "Max. ist 50 Zeichen.")]
        public string Plz { get; set; }

        [Display(Name = "Wohnort")]
        [StringLength(50, ErrorMessage = "Max. ist 50 Zeichen.")]
        [Required(ErrorMessage = "Wohnort ist erforderlich!")]
        public string Place { get; set; }

        [Display(Name = "Abteilung")]
        [Required(ErrorMessage = "Abteilung ist erforderlich!")]
        [ForeignKey("AbteilungId")]
        public virtual Abteilung Abteilung { get; set; }

        public int AbteilungId { get; set; }

        [Display(Name = "Projekt")]
        [Required(ErrorMessage = "Projekt ist erforderlich!")]
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public int ProjectId { get; set; }
    }
}