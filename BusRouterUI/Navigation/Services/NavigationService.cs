using BusRouterUI.Navigation.Stores;
using BusRouterUI.ViewModels;

namespace BusRouterUI.Navigation.Services
{
    public class NavigationService<TViewModel> : INavigationService<TViewModel> where TViewModel : ViewModelsBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            if (_navigationStore.CurrentViewModel != null)
            {
                _navigationStore.CurrentViewModel.Dispose();
            }
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}