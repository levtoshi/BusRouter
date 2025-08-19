using BLL.Models;
using BLL.Services.Initializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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