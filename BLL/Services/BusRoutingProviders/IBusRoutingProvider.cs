using System.ComponentModel;

namespace BLL.Services.BusRoutingProviders
{
    public interface IBusRoutingProvider
    {
        bool IsStopped { get; set; }
        bool IsStarted { get; set; }

        event PropertyChangedEventHandler? PropertyChanged;

        void StartThreads();
        void StopThreads();
        void RestartThreads();
        void Dispose();
    }
}