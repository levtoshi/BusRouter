using BLL.Models;
using System.Drawing;

namespace BLL.Services.Initializers
{
    public static class MapInitializer
    {
        private static List<string> _busesNames;
        private static List<string> _stopsNames;
        private static List<string> _routesNames;
        private static List<PointF> _coordinates;

        public static Map InitializeMap(int busSpeedPixelsPerSecond, int busAmountOfSeats)
        {
            InitializeVariables();

            List<Stop> stops = InitializeStops();
            List<Route> routes = InitializeRoutes();
            List<Bus> buses = InitializeBuses(stops, routes, busSpeedPixelsPerSecond, busAmountOfSeats);

            Map map = new Map(stops, routes, buses);
            return map;
        }

        private static void InitializeVariables()
        {
            _busesNames = new List<string>();
            _busesNames.Add("Bus №33");
            _busesNames.Add("Bus №55");

            _stopsNames = new List<string>();
            _stopsNames.Add("Stop №1");
            _stopsNames.Add("Stop №2");
            _stopsNames.Add("Stop №3");
            _stopsNames.Add("Stop №4");

            _routesNames = new List<string>();
            _routesNames.Add("A-1");
            _routesNames.Add("A-2");
            _routesNames.Add("A-3");
            _routesNames.Add("B-1");
            _routesNames.Add("B-2");
            _routesNames.Add("B-3");

            _coordinates = new List<PointF>();
            _coordinates.Add(new PointF(50, 50));
            _coordinates.Add(new PointF(690, 50));
            _coordinates.Add(new PointF(690, 700));
            _coordinates.Add(new PointF(50, 700));
        }

        private static List<Route> InitializeRoutes()
        {
            List<Route> routes = new List<Route>();
            routes.Add(new Route(_routesNames[0], _coordinates[0], _coordinates[1]));
            routes.Add(new Route(_routesNames[1], _coordinates[1], _coordinates[2]));
            routes.Add(new Route(_routesNames[2], _coordinates[2], _coordinates[0]));
            routes.Add(new Route(_routesNames[3], _coordinates[0], _coordinates[2]));
            routes.Add(new Route(_routesNames[4], _coordinates[2], _coordinates[3]));
            routes.Add(new Route(_routesNames[5], _coordinates[3], _coordinates[0]));
            return routes;
        }

        private static List<Stop> InitializeStops()
        {
            int peopleAmount = 2;

            List<Stop> stops = new List<Stop>();
            
            List<List<BusStatistic>> stopsPeopleOnStop = new List<List<BusStatistic>>();

            for (int i = 0; i < _stopsNames.Count; ++i)
            {
                List<BusStatistic> busStatistics = new List<BusStatistic>();

                if (i % 2 == 1)
                {
                    List<StopStatistic> stopStatistics = new List<StopStatistic>();

                    stopStatistics.Add(new StopStatistic(_stopsNames[0], peopleAmount));
                    stopStatistics.Add(new StopStatistic(_stopsNames[2], peopleAmount));

                    busStatistics.Add(new BusStatistic(_busesNames[(i + 1) / 2 == 1 ? 0 : 1], stopStatistics, (peopleAmount * 2)));
                }
                else
                {
                    for (int j = 0; j < 2; ++j)
                    {
                        List<StopStatistic> stopStatistics = new List<StopStatistic>();

                        if (j == 0)
                        {
                            for (int k = 1; k < 3; ++k)
                            {
                                stopStatistics.Add(new StopStatistic(_stopsNames[i == 0 ? k : k - 1], peopleAmount));
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                for (int k = 2; k < 4; ++k)
                                {
                                    stopStatistics.Add(new StopStatistic(_stopsNames[k], peopleAmount));
                                }
                            }
                            else
                            {
                                stopStatistics.Add(new StopStatistic(_stopsNames[0], peopleAmount));
                                stopStatistics.Add(new StopStatistic(_stopsNames[3], peopleAmount));
                            }
                        }

                        busStatistics.Add(new BusStatistic(_busesNames[j], stopStatistics, (peopleAmount * 2)));
                    }
                }
                stopsPeopleOnStop.Add(busStatistics);
            }

            for (int i = 0; i < _stopsNames.Count; ++i)
            {
                stops.Add(new Stop(_stopsNames[i], stopsPeopleOnStop[i], _coordinates[i]));
            }

            return stops;
        }

        private static List<Bus> InitializeBuses(List<Stop> stopsParam, List<Route> routesParam, int busSpeedPixelsPerSecond, int busAmountOfSeats)
        {
            int peopleAmount = 5;

            List<Bus> buses = new List<Bus>();

            List<List<StopStatistic>> busesStopStatistics = new List<List<StopStatistic>>();

            for (int i = 0; i < _busesNames.Count; ++i)
            {
                List<StopStatistic> stopStatistic = new List<StopStatistic>();
                for (int j = 0; j < _stopsNames.Count; ++j)
                {
                    if (i == 1 && j == 1 || i == 0 && j == 3) { }
                    else
                    {
                        stopStatistic.Add(new StopStatistic(_stopsNames[j], peopleAmount));
                    }
                }

                List<Route> routes = new List<Route>();
                for (int j = 0; j < 3; ++j)
                {
                    routes.Add(routesParam[i * 3 + j]);
                }

                List<Stop> stops = new List<Stop>();

                for (int j = 0; j < 3; ++j)
                {
                    stops.Add(stopsParam.First(a => routes[j].StartPoint == a.Coordinates));
                }

                buses.Add(new Bus(_busesNames[i],
                    busAmountOfSeats,
                    busSpeedPixelsPerSecond,
                    stopStatistic,
                    _coordinates[0],
                    routesParam[i * 3],
                    routes,
                    stops));
            }

            return buses;
        }
    }
}