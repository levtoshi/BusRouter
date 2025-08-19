using BLL.Models;
using BLL.Services.BusRoutingProviders;
using BusRouterUI.Commands;
using BusRouterUI.Stores;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows.Input;

namespace BusRouterUI.ViewModels
{
    public class BusRouterViewModel : ViewModelsBase
    {
        private readonly IBusRoutingProvider _busRoutingProvider;
        private readonly BusRouterControlContextStore _busRouterControlContextStore;

        public IEnumerable<BusViewModel> Buses { get; }
        public IEnumerable<StopViewModel> Stops { get; }

        private readonly ICommand _startCommand;
        private readonly ICommand _restartCommand;
        public ICommand StartOrRestartCommand { get; set; }
        public ICommand StopCommand { get; }

        private bool _isStarted => _busRouterControlContextStore.BusRouterControlContextObject.IsStarted;
        public bool IsStarted
        {
            get => _isStarted;
        }

        public BusRouterViewModel(MapStore mapStore, IBusRoutingProvider busRoutingProvider, BusRouterControlContextStore busRouterControlContextStore)
        {
            _busRoutingProvider = busRoutingProvider;
            _busRouterControlContextStore = busRouterControlContextStore;

            _busRouterControlContextStore.BusRouterControlContextObject.PropertyChanged += OnControlContextPropertyChanged;

            _startCommand = new StartCommand(_busRoutingProvider, _busRouterControlContextStore);
            _restartCommand = new RestartCommand(_busRoutingProvider, _busRouterControlContextStore);
            StartOrRestartCommand = _startCommand;
            StopCommand = new StopCommand(_busRoutingProvider, _busRouterControlContextStore);

            Buses = new ObservableCollection<BusViewModel>(
                mapStore.MapObject.Buses.Select(bus => new BusViewModel(bus, GetBusImagePath(bus.Name)))
            );

            Stops = new ObservableCollection<StopViewModel>(
                mapStore.MapObject.Stops.Select(stop => new StopViewModel(stop))
            );
        }

        private string GetBusImagePath(string busName)
        {
            return $"pack://application:,,,/Resources/Images/{busName.Replace(" №", "").ToLower()}.png";
        }

        private void OnControlContextPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BusRouterControlContext.IsStarted))
            {
                StartOrRestartCommand = _busRouterControlContextStore.BusRouterControlContextObject.IsStarted ? _restartCommand : _startCommand;
                OnPropertyChanged(nameof(StartOrRestartCommand));
                OnPropertyChanged(nameof(IsStarted));
            }
        }

        public override void Dispose()
        {
            _busRoutingProvider.Dispose();

            _busRouterControlContextStore.BusRouterControlContextObject.PropertyChanged -= OnControlContextPropertyChanged;

            foreach (BusViewModel bus in Buses)
            {
                bus.Dispose();
            }

            foreach (StopViewModel stop in Stops)
            {
                stop.Dispose();
            }

            if (_restartCommand is IDisposable disposable1)
            {
                disposable1.Dispose();
            }
            if (_startCommand is IDisposable disposable2)
            {
                disposable2.Dispose();
            }
            if (StopCommand is IDisposable disposable3)
            {
                disposable3.Dispose();
            }

            base.Dispose();
        }
    }
}