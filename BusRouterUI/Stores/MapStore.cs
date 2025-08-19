using BLL.Models;
using BLL.Services.Initializers;

namespace BusRouterUI.Stores
{
    public class MapStore
    {
        private Map _mapObject;
        public Map MapObject
        {
            get
            {
                return _mapObject;
            }
        }

        public MapStore()
        {
            _mapObject = MapInitializer.InitializeMap();
        }
    }
}