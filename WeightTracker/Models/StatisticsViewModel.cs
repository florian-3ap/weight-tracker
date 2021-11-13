using System.Collections.Generic;

namespace WeightTracker.Models
{
    public class StatisticsViewModel
    {
        public string Table { get; set; }
        public string Theme { get; set; }
        public int Count { get; set; }

        public List<StatisticsViewModel> StatisticList { get; set; }

        public StatisticsViewModel()
        {
            StatisticList = new List<StatisticsViewModel>();
        }
    }
}