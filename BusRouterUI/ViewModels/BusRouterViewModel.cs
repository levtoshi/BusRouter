using BLL.Models;
using BLL.Services.BusRoutingProviders;
using BusRouterUI.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows.Input;

namespace BusRouterUI.ViewModels
{
    public class BusRouterViewModel : ViewModelsBase
    {
        private readonly IBusRoutingProvider _busRoutingProvider;

        public IEnumerable<BusViewModel> Buses { get; }
        public IEnumerable<StopViewModel> Stops { get; }

        private readonly ICommand _startCommand;
        private readonly ICommand _restartCommand;
        public ICommand StartOrRestartCommand { get; set; }
        public ICommand StopCommand { get; }

        private bool _isStarted => _busRoutingProvider.IsStarted;
        public bool IsStarted
        {
            get => _isStarted;
        }

        public BusRouterViewModel(Map map, IBusRoutingProvider busRoutingProvider)
        {
            _busRoutingProvider = busRoutingProvider;
            _busRoutingProvider.PropertyChanged += OnServiceChanged;

            _startCommand = new StartCommand(_busRoutingProvider);
            _restartCommand = new RestartCommand(_busRoutingProvider);
            StartOrRestartCommand = _startCommand;
            StopCommand = new StopCommand(_busRoutingProvider);

            Buses = new ObservableCollection<BusViewModel>(
                map.Buses.Select(bus => new BusViewModel(bus, GetBusImagePath(bus.Name)))
            );

            Stops = new ObservableCollection<StopViewModel>(
                map.Stops.Select(stop => new StopViewModel(stop))
            );
        }

        private string GetBusImagePath(string busName)
        {
            return $"pack://application:,,,/Resources/Images/{busName.Replace(" №", "").ToLower()}.png";
        }

        private void OnServiceChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BusRoutingProvider.IsStarted))
            {
                StartOrRestartCommand = _busRoutingProvider.IsStarted ? _restartCommand : _startCommand;
                OnPropertyChanged(nameof(StartOrRestartCommand));
                OnPropertyChanged(nameof(IsStarted));
            }
        }

        public override void Dispose()
        {
            _busRoutingProvider.Dispose();

            _busRoutingProvider.PropertyChanged -= OnServiceChanged;

            foreach (BusViewModel bus in Buses)
            {
                bus.Dispose();
            }

            foreach (StopViewModel stop in Stops)
            {
                stop.Dispose();
            }

            (_restartCommand as CommandBase).Dispose();
            (_startCommand as CommandBase).Dispose();
            (StopCommand as CommandBase).Dispose();

            base.Dispose();
        }
    }
}