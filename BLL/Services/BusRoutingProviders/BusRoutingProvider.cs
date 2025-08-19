using BLL.InterfaceAccessors;
using BLL.Models;
using BLL.Services.BusServices;
using BLL.Services.StopServices;

namespace BLL.Services.BusRoutingProviders
{
    public class BusRoutingProvider : IBusRoutingProvider, IDisposable
    {
        private readonly Map _mapObject;
        private readonly BusRouterControlContext _busRouterControlContext;
        private readonly IBusService _busService;
        private readonly IStopService _stopService;

        public BusRoutingProvider(IMapAccessor mapAccessor, IBusRouterControlContextAccessor busRouterControlContextAccessor, IBusService busService, IStopService stopService)
        {
            _mapObject = mapAccessor.GetMap();

            _busRouterControlContext = busRouterControlContextAccessor.GetBusRouterControlContext();

            _busService = busService;
            _stopService = stopService;

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
            _busRouterControlContext.IsStopped = false;
            _busRouterControlContext.IsStarted = true;
        }

        public void StopThreads()
        {
            _busRouterControlContext.StopTasks.Reset();
            _busRouterControlContext.IsStopped = true;
        }

        public void RestartThreads()
        {
            _busRouterControlContext.StopTasks.Set();
            _busRouterControlContext.IsStopped = false;
        }

        public void Dispose()
        {
            try
            {
                _busRouterControlContext.BusRouterCancellationTokenSource.Cancel();
                _busRouterControlContext.BusRouterCancellationTokenSource.Dispose();
            }
            catch { }

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