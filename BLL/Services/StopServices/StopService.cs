using BLL.InterfaceAccessors;
using BLL.Models;

namespace BLL.Services.StopServices
{
    public class StopService : IStopService
    {
        private readonly Map _mapObject;
        private readonly BusRouterControlContext _busRouterControlContext;
        private readonly int _stopPeopleIncreaseMS;

        public StopService(IMapAccessor mapAccessor, IBusRouterControlContextAccessor busRouterControlContextAccessor, IBusRoutingContextAccessor busRoutingContextAccessor)
        {
            _mapObject = mapAccessor.GetMap();
            _busRouterControlContext = busRouterControlContextAccessor.GetBusRouterControlContext();
            BusRoutingContext busRoutingContext = busRoutingContextAccessor.GetBusRoutingContext();
            _stopPeopleIncreaseMS = busRoutingContext.StopPeopleIncreaseMS;
        }

        public void GenerateStopPeople(object obj)
        {
            int index = (int)obj;

            Stop stop = _mapObject.Stops[index];
            List<Bus> buses = _mapObject.Buses.Where(a => a.Stops.FirstOrDefault(s => s.Name == _mapObject.Stops[index].Name) != null).ToList();
            int sum = 0;

            while (!_busRouterControlContext.BusRouterCancellationToken.IsCancellationRequested)
            {
                try
                {
                    _busRouterControlContext.StopsResetEvents[index].Wait();
                }
                catch { return; }

                if (!_busRouterControlContext.StopTasks.SafeWaitHandle.IsClosed)
                {
                    _busRouterControlContext.StopTasks.WaitOne();
                }
                else
                {
                    return;
                }

                for (int i = 0; i < buses.Count; ++i)
                {
                    for (int j = 0; j < stop.PeopleOnStop[i].StopStatistic.Count; ++j)
                    {
                        stop.PeopleOnStop[i].StopStatistic[j].PeopleAmount++;
                        sum++;
                    }
                    stop.PeopleOnStop[i].PeopleAmount += sum;
                    sum = 0;
                }

                Thread.Sleep(_stopPeopleIncreaseMS);
            }
        }
    }
}