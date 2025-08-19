using BLL.Models;
using BLL.Services.BusRoutingProviders;
using BusRouterUI.Stores;
using System.ComponentModel;

namespace BusRouterUI.Commands
{
    public class RestartCommand : CommandBase, IDisposable
    {
        private readonly IBusRoutingProvider _busRoutingProvider;
        private readonly BusRouterControlContextStore _busRouterControlContextStore;

        public RestartCommand(IBusRoutingProvider busRouterService, BusRouterControlContextStore busRouterControlContextStore)
        {
            _busRoutingProvider = busRouterService;
            _busRouterControlContextStore = busRouterControlContextStore;

            _busRouterControlContextStore.BusRouterControlContextObject.PropertyChanged += OnControlContextPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            _busRoutingProvider.RestartThreads();
        }

        public override bool CanExecute(object? parameter)
        {
            return _busRouterControlContextStore.BusRouterControlContextObject.IsStopped && base.CanExecute(parameter);
        }

        private void OnControlContextPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BusRouterControlContext.IsStopped))
            {
                OnCanExecuteChanged();
            }
        }

        public void Dispose()
        {
            _busRouterControlContextStore.BusRouterControlContextObject.PropertyChanged -= OnControlContextPropertyChanged;
        }
    }
}