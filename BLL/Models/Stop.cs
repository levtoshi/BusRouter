using System.Drawing;

namespace BLL.Models
{
    public class Stop
    {
        public string Name { get; }
        public List<BusStatistic> PeopleOnStop { get; }
        public PointF Coordinates { get; }

        public Stop(string stopName, List<BusStatistic> busStatistics, PointF stopCoordinates)
        {
            Name = stopName;
            PeopleOnStop = busStatistics;
            Coordinates = stopCoordinates;
        }
    }
}