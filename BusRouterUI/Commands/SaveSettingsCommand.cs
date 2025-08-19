using BLL.Models;
using BLL.Services.BusRoutingProviders;
using BusRouterUI.Navigation.Services;
using BusRouterUI.Stores;
using BusRouterUI.ViewModels;
using System.ComponentModel;

namespace BusRouterUI.Commands
{
    public class SaveSettingsCommand : CommandBase, IDisposable
    {
        private readonly SetBusRoutingViewModel _setBusRoutingViewModel;
        private readonly INavigationService<BusRouterViewModel> _navigationService;
        private readonly MapStore _mapStore;
        private readonly BusRoutingContextStore _busRoutingContextStore;

        public SaveSettingsCommand(SetBusRoutingViewModel setBusRoutingViewModel, INavigationService<BusRouterViewModel> navigationService, MapStore mapStore, BusRoutingContextStore busRoutingContextStore)
        { 
            _setBusRoutingViewModel = setBusRoutingViewModel;
            _setBusRoutingViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _navigationService = navigationService;
            _mapStore = mapStore;
            _busRoutingContextStore = busRoutingContextStore;
        }

        public override void Execute(object? parameter)
        {
            _busRoutingContextStore.Update(_setBusRoutingViewModel.StopWaitMS, _setBusRoutingViewModel.UpdateMapMS, _setBusRoutingViewModel.StopPeopleIncreaseMS);

            foreach (Bus bus in _mapStore.MapObject.Buses)
            {
                bus.SpeedPixelsPerSecond = _setBusRoutingViewModel.SpeedPixelsPerSecond;
                bus.AmountOfSeats = _setBusRoutingViewModel.AmountOfSeats;
            }

            _navigationService.Navigate();
        }

        public override bool CanExecute(object? parameter)
        {
            return _setBusRoutingViewModel.CanSetBusRouting && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SetBusRoutingViewModel.CanSetBusRouting))
            {
                OnCanExecuteChanged();
            }
        }

        public void Dispose()
        {
            _setBusRoutingViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
    }
}