using BusRouterUI.ViewModels;

namespace BusRouterUI.Navigation.Services
{
    public interface INavigationService
    {
        void NavigateTo<TViewModel>(params object[] parameters) where TViewModel : ViewModelsBase;
    }
}