using System.ComponentModel;

namespace BLL.Models
{
    public class BusStatistic : INotifyPropertyChanged
    {
        public string BusName { get; }
        public List<StopStatistic> StopStatistic { get; }

        private int _peopleAmount;
        public int PeopleAmount
        {
            get => _peopleAmount;
            set
            {
                if (_peopleAmount != value)
                {
                    _peopleAmount = value;
                    OnPropertyChanged(nameof(PeopleAmount));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public BusStatistic(string busName, List<StopStatistic> stopStatistics, int peopleAmount)
        {
            BusName = busName;
            StopStatistic = stopStatistics;
            PeopleAmount = peopleAmount;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}