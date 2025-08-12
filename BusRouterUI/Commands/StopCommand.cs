using BLL.Services.BusRoutingProviders;
using System.ComponentModel;

namespace BusRouterUI.Commands
{
    public class StopCommand : CommandBase
    {
        private readonly IBusRoutingProvider _busRoutingProvider;

        public override void Execute(object? parameter)
        {
            _busRoutingProvider.StopThreads();
        }

        public StopCommand(IBusRoutingProvider busRouterService)
        {
            _busRoutingProvider = busRouterService;
            _busRoutingProvider.PropertyChanged += OnServiceChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !_busRoutingProvider.IsStopped && base.CanExecute(parameter);
        }

        private void OnServiceChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BusRoutingProvider.IsStopped))
            {
                OnCanExecuteChanged();
            }
        }

        public override void Dispose()
        {
            _busRoutingProvider.PropertyChanged -= OnServiceChanged;
            base.Dispose();
        }
    }
}