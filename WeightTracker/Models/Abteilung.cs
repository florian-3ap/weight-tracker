using System.ComponentModel.DataAnnotations;

namespace WeightTracker.Models
{
    public class Abteilung
    {
        [Key] public int Id { get; set; }

        [Display(Name = "Abteilung")]
        [StringLength(50, ErrorMessage = "Max. ist 50 Zeichen.")]
        public string Name { get; set; }
    }
}