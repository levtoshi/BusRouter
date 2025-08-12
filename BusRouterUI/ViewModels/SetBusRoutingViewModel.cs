using BLL.Models;
using BusRouterUI.Commands;
using BusRouterUI.Navigation.Services;
using System.Collections;
using System.ComponentModel;
using System.Windows.Input;

namespace BusRouterUI.ViewModels
{
    public class SetBusRoutingViewModel : ViewModelsBase, INotifyDataErrorInfo
    {
        private int _stopWaitMS;
        public int StopWaitMS
        {
            get
            {
                return _stopWaitMS;
            }
            set
            {
                if (_stopWaitMS != value)
                {
                    _stopWaitMS = value;
                    OnPropertyChanged(nameof(StopWaitMS));

                    ClearErrors(nameof(StopWaitMS));

                    if (!HasStopWaitMSGreaterThanZero)
                    {
                        AddError("The stop wait milliseconds amount must be greater than 0", nameof(StopWaitMS));
                    }

                    OnPropertyChanged(nameof(CanSetBusRouting));
                }
            }
        }

        private int _speedPixelsPerSecond;
        public int SpeedPixelsPerSecond
        {
            get
            {
                return _speedPixelsPerSecond;
            }
            set
            {
                _speedPixelsPerSecond = value;
                OnPropertyChanged(nameof(SpeedPixelsPerSecond));

                ClearErrors(nameof(SpeedPixelsPerSecond));

                if (!HasSpeedPixelsPerSecondGreaterThanZero)
                {
                    AddError("The speed of bus must be greater than 0", nameof(SpeedPixelsPerSecond));
                }

                OnPropertyChanged(nameof(CanSetBusRouting));
            }
        }

        private int _updateMapMS;
        public int UpdateMapMS
        {
            get
            {
                return _updateMapMS;
            }
            set
            {
                _updateMapMS = value;
                OnPropertyChanged(nameof(UpdateMapMS));

                ClearErrors(nameof(UpdateMapMS));

                if (!HasUpdateMapMSGreaterThanZero)
                {
                    AddError("The update map milliseconds amount must be greater than 0", nameof(UpdateMapMS));
                }

                OnPropertyChanged(nameof(CanSetBusRouting));
            }
        }

        private int _stopPeopleIncreaseMS;
        public int StopPeopleIncreaseMS
        {
            get
            {
                return _stopPeopleIncreaseMS;
            }
            set
            {
                _stopPeopleIncreaseMS = value;
                OnPropertyChanged(nameof(StopPeopleIncreaseMS));

                ClearErrors(nameof(StopPeopleIncreaseMS));

                if (!HasStopPeopleIncreaseMSGreaterThanZero)
                {
                    AddError("The stop people increase milliseconds amount must be greater than 0", nameof(StopPeopleIncreaseMS));
                }

                OnPropertyChanged(nameof(CanSetBusRouting));
            }
        }

        private int _amountOfSeats;
        public int AmountOfSeats
        {
            get
            {
                return _amountOfSeats;
            }
            set
            {
                _amountOfSeats = value;
                OnPropertyChanged(nameof(AmountOfSeats));

                ClearErrors(nameof(AmountOfSeats));

                if (!HasAmountOfSeatsGreaterThanZero)
                {
                    AddError("The amount of seats must be greater than 0", nameof(AmountOfSeats));
                }

                OnPropertyChanged(nameof(CanSetBusRouting));
            }
        }

        public bool CanSetBusRouting =>
            HasStopWaitMSGreaterThanZero &&
            HasSpeedPixelsPerSecondGreaterThanZero &&
            HasUpdateMapMSGreaterThanZero &&
            HasStopPeopleIncreaseMSGreaterThanZero &&
            HasAmountOfSeatsGreaterThanZero &&
            !HasErrors;

        private bool HasStopWaitMSGreaterThanZero => StopWaitMS > 0;
        private bool HasSpeedPixelsPerSecondGreaterThanZero => SpeedPixelsPerSecond > 0;
        private bool HasUpdateMapMSGreaterThanZero => UpdateMapMS > 0;
        private bool HasStopPeopleIncreaseMSGreaterThanZero => StopPeopleIncreaseMS > 0;
        private bool HasAmountOfSeatsGreaterThanZero => AmountOfSeats > 0;

        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public ICommand SaveCommand { get; }

        public SetBusRoutingViewModel(Map map, INavigationService navigationService)
        {
            SaveCommand = new SaveSettingsCommand(this, map, navigationService);
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();

            StopWaitMS = 2000;
            SpeedPixelsPerSecond = 150;
            UpdateMapMS = 10;
            StopPeopleIncreaseMS = 1250;
            AmountOfSeats = 50;
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void AddError(string errorMessage, string propertyName)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }

            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);

            OnErrorsChanged(propertyName);
        }

        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);

            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public override void Dispose()
        {
            (SaveCommand as CommandBase).Dispose();
            base.Dispose();
        }
    }
}