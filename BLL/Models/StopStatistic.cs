using System.ComponentModel;

namespace BLL.Models
{
    public class StopStatistic : INotifyPropertyChanged
    {
        public string StopName { get; set; }

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

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}