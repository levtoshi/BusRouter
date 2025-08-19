using BusRouterUI.Navigation.Services;
using BusRouterUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using BusRouterUI.HostBuilders;

namespace BusRouterUI
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddStores()
                .AddAccessors()
                .AddServices()
                .AddViewModels()
                .AddNavigation()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs arg)
        {
            await _host.StartAsync();

            INavigationService<SetBusRoutingViewModel> navigationService = _host.Services.GetRequiredService<INavigationService<SetBusRoutingViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(arg);
        }
        
        protected override async void OnExit(ExitEventArgs e)
        {
            MainViewModel mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
            mainViewModel.Dispose();

            await _host.StopAsync();
            _host.Dispose();
            
            base.OnExit(e);
        }
    }
}