using System.ComponentModel;
using System.Drawing;

namespace BLL.Models
{
    public class Bus : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public int AmountOfSeats { get; set; }
        public int SpeedPixelsPerSecond { get; set; }
        public List<StopStatistic> PeopleOnBus { get; set; }

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
        public List<Route> Routes { get; set; }
        public List<Stop> Stops { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}