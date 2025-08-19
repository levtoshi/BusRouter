using BLL.InterfaceAccessors;
using BLL.Models;
using BusRouterUI.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusRouterUI.Accessors
{
    public class BusRouterControlContextStoreAccessor : IBusRouterControlContextAccessor
    {
        private readonly BusRouterControlContextStore _busRouterControlContextStore;

        public BusRouterControlContextStoreAccessor(BusRouterControlContextStore busRoutingContextStore)
        {
            _busRouterControlContextStore = busRoutingContextStore;
        }

        public BusRouterControlContext GetBusRouterControlContext()
        {
            return _busRouterControlContextStore.BusRouterControlContextObject;
        }
    }
}