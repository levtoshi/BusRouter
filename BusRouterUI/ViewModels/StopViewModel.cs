using BLL.Models;
using System.Collections.ObjectModel;

namespace BusRouterUI.ViewModels
{
    public class StopViewModel : ViewModelsBase
    {
        public string Name { get; }
        public IEnumerable<BusStatisticViewModel> BusStatistics { get; }

        public StopViewModel(Stop stop)
        {
            Name = stop.Name;
            BusStatistics = new ObservableCollection<BusStatisticViewModel>(
                stop.PeopleOnStop.Select(s => new BusStatisticViewModel(s))
            );
        }

        public override void Dispose()
        {
            foreach (BusStatisticViewModel busStatistic in BusStatistics)
            {
                busStatistic.Dispose();
            }

            base.Dispose();
        }
    }
}