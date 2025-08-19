using System.Drawing;

namespace BLL.Models
{
    public class Route
    {
        public string RouteName { get; }
        public PointF StartPoint { get; }
        public PointF EndPoint { get; }

        public Route(string routeName, PointF startPoint, PointF endPoint)
        {
            RouteName = routeName;
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }
}