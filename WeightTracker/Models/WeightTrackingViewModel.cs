using System.Collections.Generic;

namespace WeightTracker.Models
{
    public class WeightTrackingViewModel
    {
        public Dictionary<Person, List<WeightTracking>> PersonTracking { get; set; }
    }
}