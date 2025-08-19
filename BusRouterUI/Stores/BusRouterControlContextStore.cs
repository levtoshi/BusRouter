using BLL.Models;
using BLL.Services.Initializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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