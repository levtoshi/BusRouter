using BusRouterUI.ViewModels;

namespace BusRouterUI.Navigation.Services
{
    public interface INavigationService<TViewModel> where TViewModel : ViewModelsBase
    {
        void Navigate();
    }
}