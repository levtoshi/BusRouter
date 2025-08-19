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