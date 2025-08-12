using System.ComponentModel;

namespace BLL.Models
{
    public class BusStatistic : INotifyPropertyChanged
    {
        public string BusName { get; set; }
        public List<StopStatistic> StopStatistic { get; set; }

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

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}