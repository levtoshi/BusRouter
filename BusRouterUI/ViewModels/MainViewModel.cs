using BusRouterUI.Navigation.Stores;

namespace BusRouterUI.ViewModels
{
    public class MainViewModel : ViewModelsBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelsBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public override void Dispose()
        {
            _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;
            base.Dispose();
        }
    }
}