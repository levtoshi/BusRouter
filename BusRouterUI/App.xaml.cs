using BLL.Models;
using BLL.Services.Initializers;
using BusRouterUI.Navigation.Services;
using BusRouterUI.Navigation.Stores;
using BusRouterUI.ViewModels;
using System.Windows;

namespace BusRouterUI
{
    public partial class App : Application
    {
        private readonly MainViewModel _mainViewModel;

        public App()
        {
            Map mapObject = MapInitializer.InitializeMap();
            NavigationStore navigationStore = new NavigationStore();

            INavigationService navigationService = new NavigationService(navigationStore);

            navigationStore.CurrentViewModel = new SetBusRoutingViewModel(mapObject, navigationService);
            _mainViewModel = new MainViewModel(navigationStore);
        }

        protected override void OnStartup(StartupEventArgs arg)
        {
            MainWindow = new MainWindow()
            {
                DataContext = _mainViewModel
            };

            if (MainWindow != null)
            {
                MainWindow.Show();
            }

            base.OnStartup(arg);
        }
        
        protected override void OnExit(ExitEventArgs e)
        {
            _mainViewModel.Dispose();
            base.OnExit(e);
        }
    }
}