using BLL.Models;

namespace BusRouterUI.Stores
{
    public class BusRoutingContextStore
    {
        private BusRoutingContext _busRoutingContextObject;
        public BusRoutingContext BusRoutingContextObject
        {
            get
            {
                return _busRoutingContextObject;
            }
        }

        public void Update(int stopWaitMS, int updateMapMS, int stopPeopleIncreaseMS)
        {
            _busRoutingContextObject = new BusRoutingContext(stopWaitMS, updateMapMS, stopPeopleIncreaseMS);
        }
    }
}