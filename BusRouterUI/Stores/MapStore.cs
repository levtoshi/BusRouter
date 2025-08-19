using BLL.Models;
using BLL.Services.Initializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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