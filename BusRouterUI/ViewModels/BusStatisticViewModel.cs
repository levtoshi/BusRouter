using BLL.Models;

namespace BusRouterUI.ViewModels
{
    public class BusStatisticViewModel : ViewModelsBase
    {
        private readonly BusStatistic _busStatistic;
        public string BusName { get; }

        private int _peopleAmount => _busStatistic.PeopleAmount;
        public int PeopleAmount
        {
            get => _peopleAmount;
        }

        public BusStatisticViewModel(BusStatistic busStatistic)
        {
            BusName = busStatistic.BusName;
            _busStatistic = busStatistic;
            _busStatistic.PropertyChanged += OnBusStatisticChanged;
        }

        private void OnBusStatisticChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_busStatistic.PeopleAmount))
            {
                OnPropertyChanged(nameof(PeopleAmount));
            }
        }

        public override void Dispose()
        {
            _busStatistic.PropertyChanged -= OnBusStatisticChanged;
            base.Dispose();
        }
    }
}