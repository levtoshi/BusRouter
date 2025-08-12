using BLL.Models;

namespace BusRouterUI.ViewModels
{
    public class StopStatisticViewModel : ViewModelsBase
    {
        private readonly StopStatistic _stopStatistic;

        public string StopName { get; }

        private int _peopleAmount => _stopStatistic.PeopleAmount;
        public int PeopleAmount
        {
            get => _peopleAmount;
        }

        public StopStatisticViewModel(StopStatistic stopStatistic)
        {
            StopName = stopStatistic.StopName;
            _stopStatistic = stopStatistic;
            _stopStatistic.PropertyChanged += OnStopStatisticChanged;
        }

        private void OnStopStatisticChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_stopStatistic.PeopleAmount))
            {
                OnPropertyChanged(nameof(PeopleAmount));
            }
        }

        public override void Dispose()
        {
            _stopStatistic.PropertyChanged -= OnStopStatisticChanged;
            base.Dispose();
        }
    }
}