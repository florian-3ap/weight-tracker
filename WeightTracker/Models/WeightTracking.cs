using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeightTracker.Models
{
    public class WeightTracking
    {
        [Key] public int Id { get; set; }

        [Display(Name = "Gewicht KG")]
        [Required(ErrorMessage = "Gewicht ist erforderlich!")]
        [Range(1.0, 500, ErrorMessage = "Gewicht zwischen 1 und 500 KG")]
        public double Weight { get; set; }

        [Display(Name = "Datum")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "Datum ist erforderlich!")]
        public DateTime Date { get; set; }

        public int PersonId { get; set; }

        [Display(Name = "Person")]
        [Required(ErrorMessage = "Person ist erforderlich!")]
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }
}