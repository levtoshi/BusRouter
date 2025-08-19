using BLL.InterfaceAccessors;
using BLL.Models;
using BusRouterUI.Stores;

namespace BusRouterUI.Accessors
{
    public class BusRoutingContextStoreAccessor : IBusRoutingContextAccessor
    {
        private readonly BusRoutingContextStore _busRoutingContextStore;

        public BusRoutingContextStoreAccessor(BusRoutingContextStore busRoutingContextStore)
        {
            _busRoutingContextStore = busRoutingContextStore;
        }

        public BusRoutingContext GetBusRoutingContext()
        {
            return _busRoutingContextStore.BusRoutingContextObject;
        }
    }
}