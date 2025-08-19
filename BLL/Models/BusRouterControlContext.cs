using System.ComponentModel;

namespace BLL.Models
{
    public class BusRouterControlContext : INotifyPropertyChanged
    {
        public CancellationTokenSource BusRouterCancellationTokenSource { get; }
        public CancellationToken BusRouterCancellationToken { get; }
        public ManualResetEvent StopTasks { get; }
        public List<ManualResetEventSlim> StopsResetEvents { get; }

        private bool _isStopped;
        public bool IsStopped
        {
            get => _isStopped;
            set
            {
                _isStopped = value;
                OnPropertyChanged(nameof(IsStopped));
            }
        }

        private bool _isStarted;
        public bool IsStarted
        {
            get => _isStarted;
            set
            {
                _isStarted = value;
                OnPropertyChanged(nameof(IsStarted));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public BusRouterControlContext()
        {
            BusRouterCancellationTokenSource = new CancellationTokenSource();
            BusRouterCancellationToken = BusRouterCancellationTokenSource.Token;
            StopTasks = new ManualResetEvent(true);
            StopsResetEvents = new List<ManualResetEventSlim>();

            IsStopped = true;
            IsStarted = false;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}