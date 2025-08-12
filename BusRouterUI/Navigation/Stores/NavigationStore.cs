using BusRouterUI.ViewModels;

namespace BusRouterUI.Navigation.Stores
{
    public class NavigationStore
    {
        private ViewModelsBase _currentViewModel;
        public ViewModelsBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action? CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}