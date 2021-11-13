using System.ComponentModel.DataAnnotations;

namespace WeightTracker.Models
{
    public class Project
    {
        [Key] public int Id { get; set; }

        [Display(Name = "Projekt")]
        [StringLength(50, ErrorMessage = "Max. ist 50 Zeichen.")]
        public string Name { get; set; }
    }
}