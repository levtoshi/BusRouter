using BLL.Models;
using BLL.Services.BusRoutingProviders;
using BusRouterUI.Navigation.Services;
using BusRouterUI.ViewModels;
using System.ComponentModel;

namespace BusRouterUI.Commands
{
    public class SaveSettingsCommand : CommandBase
    {
        private readonly SetBusRoutingViewModel _setBusRoutingViewModel;
        private readonly Map _mapObject;
        private readonly INavigationService _navigationService;

        public SaveSettingsCommand(SetBusRoutingViewModel setBusRoutingViewModel, Map map, INavigationService navigationService)
        { 
            _setBusRoutingViewModel = setBusRoutingViewModel;
            _setBusRoutingViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _mapObject = map;
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            IBusRoutingProvider busRoutingProvider = new BusRoutingProvider(
                _mapObject,
                new BusRoutingContext(_setBusRoutingViewModel.StopWaitMS, _setBusRoutingViewModel.UpdateMapMS, _setBusRoutingViewModel.StopPeopleIncreaseMS));

            foreach (Bus bus in _mapObject.Buses)
            {
                bus.SpeedPixelsPerSecond = _setBusRoutingViewModel.SpeedPixelsPerSecond;
                bus.AmountOfSeats = _setBusRoutingViewModel.AmountOfSeats;
            }

            _navigationService.NavigateTo<BusRouterViewModel>(_mapObject, busRoutingProvider);
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

        public override void Dispose()
        {
            _setBusRoutingViewModel.PropertyChanged -= OnViewModelPropertyChanged;
            base.Dispose();
        }
    }
}