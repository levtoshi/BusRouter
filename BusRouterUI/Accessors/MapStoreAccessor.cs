using BLL.InterfaceAccessors;
using BLL.Models;
using BusRouterUI.Stores;

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