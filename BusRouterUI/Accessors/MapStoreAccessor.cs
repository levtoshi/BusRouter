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
    public class MapStoreAccessor : IMapAccessor
    {
        private readonly MapStore _mapStore;

        public MapStoreAccessor(MapStore mapStore)
        {
            _mapStore = mapStore;
        }

        public Map GetMap()
        {
            return _mapStore.MapObject;
        }
    }
}