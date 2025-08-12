using BusRouterUI.Navigation.Stores;
using BusRouterUI.ViewModels;

namespace BusRouterUI.Navigation.Services
{
    public class NavigationService : INavigationService
    {
        private readonly NavigationStore _navigationStore;

        public NavigationService(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public void NavigateTo<TViewModel>(params object[] parameters) where TViewModel : ViewModelsBase
        {
            _navigationStore.CurrentViewModel.Dispose();

            ViewModelsBase viewModel = (TViewModel)Activator.CreateInstance(typeof(TViewModel), parameters);

            if (viewModel != null)
            {
                _navigationStore.CurrentViewModel = viewModel;
            }
        }
    }
}