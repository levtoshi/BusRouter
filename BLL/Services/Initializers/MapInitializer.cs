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

        public static Map InitializeMap()
        {
            Map map = new Map();
            InitializeVariables();
            InitializeRoutes(map);
            InitializeStops(map);
            InitializeBuses(map);
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

        private static void InitializeRoutes(Map map)
        {
            List<Route> routes = new List<Route>();
            routes.Add(new Route()
            {
                RouteName = _routesNames[0],
                StartPoint = _coordinates[0],
                EndPoint = _coordinates[1]
            });
            routes.Add(new Route()
            {
                RouteName = _routesNames[1],
                StartPoint = _coordinates[1],
                EndPoint = _coordinates[2]
            });
            routes.Add(new Route()
            {
                RouteName = _routesNames[2],
                StartPoint = _coordinates[2],
                EndPoint = _coordinates[0]
            });
            routes.Add(new Route()
            {
                RouteName = _routesNames[3],
                StartPoint = _coordinates[0],
                EndPoint = _coordinates[2]
            });
            routes.Add(new Route()
            {
                RouteName = _routesNames[4],
                StartPoint = _coordinates[2],
                EndPoint = _coordinates[3]
            });
            routes.Add(new Route()
            {
                RouteName = _routesNames[5],
                StartPoint = _coordinates[3],
                EndPoint = _coordinates[0]
            });
            map.Routes = routes;
        }

        private static void InitializeStops(Map map)
        {
            int peopleAmount = 2;

            List<Stop> stops = new List<Stop>();

            for (int i = 0; i < _stopsNames.Count; ++i)
            {
                stops.Add(new Stop()
                {
                    Name = _stopsNames[i],
                    Coordinates = _coordinates[i]
                });
            }

            for (int i = 0; i < _stopsNames.Count; ++i)
            {
                List<BusStatistic> busStatistics = new List<BusStatistic>();

                if (i % 2 == 1)
                {
                    List<StopStatistic> stopStatistics = new List<StopStatistic>();

                    stopStatistics.Add(new StopStatistic()
                    {
                        StopName = _stopsNames[0],
                        PeopleAmount = peopleAmount
                    });
                    stopStatistics.Add(new StopStatistic()
                    {
                        StopName = _stopsNames[2],
                        PeopleAmount = peopleAmount
                    });

                    busStatistics.Add(new BusStatistic()
                    {
                        BusName = _busesNames[(i + 1) / 2 == 1 ? 0 : 1],
                        StopStatistic = stopStatistics,
                        PeopleAmount = peopleAmount * 2
                    });
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
                                stopStatistics.Add(new StopStatistic()
                                {
                                    StopName = _stopsNames[i == 0 ? k : k - 1],
                                    PeopleAmount = peopleAmount
                                });
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                for (int k = 2; k < 4; ++k)
                                {
                                    stopStatistics.Add(new StopStatistic()
                                    {
                                        StopName = _stopsNames[k],
                                        PeopleAmount = peopleAmount
                                    });
                                }
                            }
                            else
                            {
                                stopStatistics.Add(new StopStatistic()
                                {
                                    StopName = _stopsNames[0],
                                    PeopleAmount = peopleAmount
                                });
                                stopStatistics.Add(new StopStatistic()
                                {
                                    StopName = _stopsNames[3],
                                    PeopleAmount = peopleAmount
                                });
                            }
                        }

                        busStatistics.Add(new BusStatistic()
                        {
                            BusName = _busesNames[j],
                            StopStatistic = stopStatistics,
                            PeopleAmount = peopleAmount * 2
                        });
                    }
                }
                stops[i].PeopleOnStop = busStatistics;
            }
            map.Stops = stops;
        }

        private static void InitializeBuses(Map map)
        {
            int peopleAmount = 5;

            List<Bus> buses = new List<Bus>();

            for (int i = 0; i < _busesNames.Count; ++i)
            {
                List<StopStatistic> stopStatistic = new List<StopStatistic>();
                for (int j = 0; j < _stopsNames.Count; ++j)
                {
                    if (i == 1 && j == 1 || i == 0 && j == 3) { }
                    else
                    {
                        stopStatistic.Add(new StopStatistic()
                        {
                            StopName = _stopsNames[j],
                            PeopleAmount = peopleAmount
                        });
                    }
                }

                List<Route> routes = new List<Route>();
                for (int j = 0; j < 3; ++j)
                {
                    routes.Add(map.Routes[i * 3 + j]);
                }

                List<Stop> stops = new List<Stop>();

                for (int j = 0; j < 3; ++j)
                {
                    stops.Add(map.Stops.First(a => routes[j].StartPoint == a.Coordinates));
                }

                buses.Add(new Bus()
                {
                    Name = _busesNames[i],
                    AmountOfSeats = 50,
                    SpeedPixelsPerSecond = 150,
                    PeopleOnBus = stopStatistic,
                    StopName = _stopsNames[0],
                    CurrentCoordinates = _coordinates[0],
                    CurrentRoute = map.Routes[i * 3],
                    Routes = routes,
                    Stops = stops
                });
            }

            map.Buses = buses;
        }
    }
}