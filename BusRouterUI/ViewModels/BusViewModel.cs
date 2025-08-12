using BLL.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BusRouterUI.ViewModels
{
    public class BusViewModel : ViewModelsBase
    {
        private readonly Bus _bus;

        public string Name { get; }
        public int AmountOfSeats { get; }
        public string ImagePath { get; }

        private float _top => _bus.CurrentCoordinates.Y;
        public float Top
        {
            get => _top;
        }

        private float _left => _bus.CurrentCoordinates.X;
        public float Left
        {
            get => _left;
        }

        public IEnumerable<StopStatisticViewModel> StopStatistics { get; }

        public BusViewModel(Bus bus, string imagePath)
        {
            _bus = bus;
            ImagePath = imagePath;
            Name = bus.Name;
            AmountOfSeats = bus.AmountOfSeats;

            StopStatistics = new ObservableCollection<StopStatisticViewModel>(
                _bus.PeopleOnBus.Select(s => new StopStatisticViewModel(s))
            );

            _bus.PropertyChanged += OnBusCoordinatesChanged;
        }

        private void OnBusCoordinatesChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_bus.CurrentCoordinates))
            {
                OnPropertyChanged(nameof(Top));
                OnPropertyChanged(nameof(Left));
            }
        }

        public override void Dispose()
        {
            _bus.PropertyChanged -= OnBusCoordinatesChanged;

            foreach (StopStatisticViewModel stopStatistic in StopStatistics)
            {
                stopStatistic.Dispose();
            }

            base.Dispose();
        }
    }
}