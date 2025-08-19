using System.ComponentModel;

namespace BLL.Models
{
    public class StopStatistic : INotifyPropertyChanged
    {
        public string StopName { get; }

        private int _peopleAmount;
        public int PeopleAmount
        {
            get => _peopleAmount;
            set
            {
                _peopleAmount = value;
                OnPropertyChanged(nameof(PeopleAmount));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public StopStatistic(string stopName, int peopleAmount)
        {
            StopName = stopName;
            PeopleAmount = peopleAmount;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}