using System.Drawing;

namespace BLL.Models
{
    public class Route
    {
        public string RouteName { get; set; }
        public PointF StartPoint { get; set; }
        public PointF EndPoint { get; set; }
    }
}