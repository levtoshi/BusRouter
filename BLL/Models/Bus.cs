using System.ComponentModel;
using System.Drawing;

namespace BLL.Models
{
    public class Bus : INotifyPropertyChanged
    {
        public string Name { get; }
        public int AmountOfSeats { get; }
        public int SpeedPixelsPerSecond { get; }
        public List<StopStatistic> PeopleOnBus { get; }

        public string StopName { get; set; }

        private PointF _currentCoordinates;
        public PointF CurrentCoordinates
        {
            get => _currentCoordinates;
            set
            {
                _currentCoordinates = value;
                OnPropertyChanged(nameof(CurrentCoordinates));
            }
        }

        public Route CurrentRoute { get; set; }
        public List<Route> Routes { get; }
        public List<Stop> Stops { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Bus(string busName, int amountOfSeats, int speedPixelsPerSecond, List<StopStatistic> peopleOnBus,
            PointF currentCoordinates, Route currentRoute, List<Route> routes, List<Stop> stops)
        {
            Name = busName;
            AmountOfSeats = amountOfSeats;
            SpeedPixelsPerSecond = speedPixelsPerSecond;
            PeopleOnBus = peopleOnBus;
            CurrentCoordinates = currentCoordinates;
            CurrentRoute = currentRoute;
            Routes = routes;
            Stops = stops;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}