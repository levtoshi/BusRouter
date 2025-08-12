namespace BLL.Models
{
    public class BusRouterControlContext
    {
        public CancellationTokenSource BusRouterCancellationTokenSource { get; }
        public CancellationToken BusRouterCancellationToken { get; }
        public ManualResetEvent StopTasks { get; }
        public List<ManualResetEventSlim> StopsResetEvents { get; }

        public BusRouterControlContext()
        {
            BusRouterCancellationTokenSource = new CancellationTokenSource();
            BusRouterCancellationToken = BusRouterCancellationTokenSource.Token;
            StopTasks = new ManualResetEvent(true);
            StopsResetEvents = new List<ManualResetEventSlim>();
        }
    }
}