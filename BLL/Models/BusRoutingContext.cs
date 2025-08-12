namespace BLL.Models
{
    public class BusRoutingContext
    {
        public int StopWaitMS { get; }
        public int UpdateMapMS { get; }
        public int StopPeopleIncreaseMS { get; }

        public BusRoutingContext(int stopWaitMS, int updateMapMS, int stopPeopleIncreaseMS)
        {
            StopWaitMS = stopWaitMS;
            UpdateMapMS = updateMapMS;
            StopPeopleIncreaseMS = stopPeopleIncreaseMS;
        }
    }
}