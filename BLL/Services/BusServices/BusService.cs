using BLL.Models;
using System.Drawing;

namespace BLL.Services.BusServices
{
    public class BusService : IBusService
    {
        private readonly Map _mapObject;
        private readonly BusRouterControlContext _busRouterControlContext;
        private readonly int _stopWaitMS;
        private readonly int _updateMapMS;

        public BusService(Map mapObject, BusRouterControlContext busRouterControlContext, int stopWaitMS, int updateMapMS)
        {
            _mapObject = mapObject;
            _busRouterControlContext = busRouterControlContext;
            _stopWaitMS = stopWaitMS;
            _updateMapMS = updateMapMS;
        }


        public void BusRouting(object obj)
        {
            int index = (int)obj;
            Bus bus = _mapObject.Buses[index];

            int stopIndex;

            while (!_busRouterControlContext.BusRouterCancellationToken.IsCancellationRequested)
            {
                if (!_busRouterControlContext.StopTasks.SafeWaitHandle.IsClosed)
                {
                    _busRouterControlContext.StopTasks.WaitOne();
                }
                else
                {
                    return;
                }

                MoveBus(bus);

                stopIndex = _mapObject.Stops.IndexOf(bus.Stops.Where(a => a.Coordinates == bus.CurrentRoute.EndPoint).First());

                try
                {
                    _busRouterControlContext.StopsResetEvents[stopIndex].Reset();
                }
                catch { return; }

                BusArrived(index, stopIndex);
                ExchangePeople(index, stopIndex);
                BusSent(index);

                Thread.Sleep(_stopWaitMS);

                try
                {
                    _busRouterControlContext.StopsResetEvents[stopIndex].Set();
                }
                catch { return; }
            }
        }

        private void MoveBus(Bus bus)
        {
            double x = bus.CurrentRoute.EndPoint.X - bus.CurrentCoordinates.X;
            double y = bus.CurrentRoute.EndPoint.Y - bus.CurrentCoordinates.Y;

            double routeLength = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

            double seconds = routeLength / bus.SpeedPixelsPerSecond * (1000 / _updateMapMS);

            double SecondX = x / seconds;
            double SecondY = y / seconds;

            for (int i = 0; i < Convert.ToInt32(seconds); ++i)
            {
                if (_busRouterControlContext.BusRouterCancellationToken.IsCancellationRequested)
                {
                    return;
                }
                if (!_busRouterControlContext.StopTasks.SafeWaitHandle.IsClosed)
                {
                    _busRouterControlContext.StopTasks.WaitOne();
                }
                else
                {
                    return;
                }
                bus.CurrentCoordinates = new PointF(
                    bus.CurrentCoordinates.X + Convert.ToSingle(SecondX),
                    bus.CurrentCoordinates.Y + Convert.ToSingle(SecondY));

                Thread.Sleep(_updateMapMS);
            }

            bus.CurrentCoordinates = new PointF(bus.CurrentRoute.EndPoint.X, bus.CurrentRoute.EndPoint.Y);
        }

        private void BusArrived(int busIndex, int stopIndex)
        {
            _mapObject.Buses[busIndex].StopName = _mapObject.Stops[stopIndex].Name;
        }

        private void ExchangePeople(int busIndex, int stopIndex)
        {
            Bus bus = _mapObject.Buses[busIndex];
            Stop stop = _mapObject.Stops[stopIndex];
            int amountOfPeople;

            lock (this)
            {
                bus.PeopleOnBus[bus.PeopleOnBus.IndexOf(bus.PeopleOnBus.First(a => a.StopName == stop.Name))].PeopleAmount = 0;

                for (int i = 0, j = 0; i < bus.Stops.Count; ++i)
                {
                    if (i != bus.Stops.IndexOf(bus.Stops.First(a => a.Name == stop.Name)))
                    {
                        amountOfPeople = bus.PeopleOnBus.Sum(a => a.PeopleAmount) + stop.PeopleOnStop[stop.PeopleOnStop.IndexOf(stop.PeopleOnStop.First(a => a.BusName == bus.Name))].StopStatistic[j].PeopleAmount <= bus.AmountOfSeats
                            ? stop.PeopleOnStop[stop.PeopleOnStop.IndexOf(stop.PeopleOnStop.First(a => a.BusName == bus.Name))].StopStatistic[j].PeopleAmount
                            : bus.AmountOfSeats - bus.PeopleOnBus.Sum(a => a.PeopleAmount);

                        bus.PeopleOnBus[i].PeopleAmount += amountOfPeople;

                        stop.PeopleOnStop[stop.PeopleOnStop.IndexOf(stop.PeopleOnStop.First(a => a.BusName == bus.Name))].PeopleAmount -= amountOfPeople;

                        stop.PeopleOnStop[stop.PeopleOnStop.IndexOf(stop.PeopleOnStop.First(a => a.BusName == bus.Name))].StopStatistic[j].PeopleAmount -= amountOfPeople;

                        if (stop.PeopleOnStop[stop.PeopleOnStop.IndexOf(stop.PeopleOnStop.First(a => a.BusName == bus.Name))].StopStatistic[j].PeopleAmount != 0)
                        {
                            break;
                        }

                        j++;
                    }
                }
            }
        }

        private void BusSent(int busIndex)
        {
            Bus bus = _mapObject.Buses[busIndex];
            if (bus.CurrentRoute == bus.Routes[bus.Routes.Count - 1])
            {
                bus.CurrentRoute = bus.Routes[0];
            }
            else
            {
                bus.CurrentRoute = bus.Routes[bus.Routes.IndexOf(bus.CurrentRoute) + 1];
            }
        }
    }
}