using System.Collections.Generic;

namespace WeightTracker.Models
{
    public class MasterDataViewModel
    {
        public List<Abteilung> AbteilungList { get; set; }
        public List<Project> ProjectList { get; set; }

        public MasterDataViewModel()
        {
            AbteilungList = new List<Abteilung>();
            ProjectList = new List<Project>();
        }
    }
}