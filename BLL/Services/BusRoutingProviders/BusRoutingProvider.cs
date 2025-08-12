using BLL.Models;
using BLL.Services.BusServices;
using BLL.Services.StopServices;
using System.ComponentModel;

namespace BLL.Services.BusRoutingProviders
{
    public class BusRoutingProvider : IBusRoutingProvider, INotifyPropertyChanged
    {
        private readonly Map _mapObject;
        private readonly BusRouterControlContext _busRouterControlContext;
        private readonly IBusService _busService;
        private readonly IStopService _stopService;

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

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BusRoutingProvider(Map map, BusRoutingContext busRoutingContext)
        {
            _mapObject = map;

            _busRouterControlContext = new BusRouterControlContext();

            _busService = new BusService(_mapObject, _busRouterControlContext, busRoutingContext.StopWaitMS, busRoutingContext.UpdateMapMS);
            _stopService = new StopService(_mapObject, _busRouterControlContext, busRoutingContext.StopPeopleIncreaseMS);

            IsStopped = true;
            IsStarted = false;

            InitializeResetEvents();
        }

        private void InitializeResetEvents()
        {
            for (int i = 0; i < _mapObject.Stops.Count; ++i)
            {
                _busRouterControlContext.StopsResetEvents.Add(new ManualResetEventSlim(true));
            }
        }

        public void StartThreads()
        {
            for (int i = 0; i < _mapObject.Buses.Count; ++i)
            {
                ThreadPool.QueueUserWorkItem(_busService.BusRouting, i);
            }
            for (int i = 0; i < _mapObject.Stops.Count; ++i)
            {
                ThreadPool.QueueUserWorkItem(_stopService.GenerateStopPeople, i);
            }
            IsStopped = false;
            IsStarted = true;
        }

        public void StopThreads()
        {
            _busRouterControlContext.StopTasks.Reset();
            IsStopped = true;
        }

        public void RestartThreads()
        {
            _busRouterControlContext.StopTasks.Set();
            IsStopped = false;
        }

        public void Dispose()
        {
            _busRouterControlContext.BusRouterCancellationTokenSource.Cancel();
            _busRouterControlContext.BusRouterCancellationTokenSource.Dispose();

            DisposeEventHandlers();
        }

        private void DisposeEventHandlers()
        {
            _busRouterControlContext.StopTasks.Dispose();

            foreach (ManualResetEventSlim manualResetEvent in _busRouterControlContext.StopsResetEvents)
            {
                manualResetEvent.Dispose();
            }
        }
    }
}