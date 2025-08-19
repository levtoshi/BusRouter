namespace BLL.Models
{
    public class Map
    {
        public List<Stop> Stops { get; }
        public List<Route> Routes { get; }
        public List<Bus> Buses { get; }
        
        public Map(List<Stop> stops, List<Route> routes, List<Bus> buses)
        {
            Stops = stops;
            Routes = routes;
            Buses = buses;
        }
    }
}