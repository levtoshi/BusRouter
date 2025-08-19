using BLL.Models;

namespace BusRouterUI.Stores
{
    public class BusRouterControlContextStore
    {
        private BusRouterControlContext _busRouterControlContextObject;
        public BusRouterControlContext BusRouterControlContextObject
        {
            get
            {
                return _busRouterControlContextObject;
            }
        }

        public BusRouterControlContextStore()
        {
            _busRouterControlContextObject = new BusRouterControlContext();
        }
    }
}