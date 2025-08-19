using System.ComponentModel;

namespace BLL.Services.BusRoutingProviders
{
    public interface IBusRoutingProvider
    {
        void StartThreads();
        void StopThreads();
        void RestartThreads();
        void Dispose();
    }
}