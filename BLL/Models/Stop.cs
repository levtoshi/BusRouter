using System.Drawing;

namespace BLL.Models
{
    public class Stop
    {
        public string Name { get; set; }
        public List<BusStatistic> PeopleOnStop { get; set; }
        public PointF Coordinates { get; set; }
    }
}