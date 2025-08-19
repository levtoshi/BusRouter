using BLL.InterfaceAccessors;
using BLL.Models;
using BLL.Services.BusRoutingProviders;
using BLL.Services.BusServices;
using BLL.Services.Initializers;
using BLL.Services.StopServices;
using BusRouterUI.Accessors;
using BusRouterUI.Navigation.Services;
using BusRouterUI.Navigation.Stores;
using BusRouterUI.Stores;
using BusRouterUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace BusRouterUI
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MapStore>();
                    services.AddSingleton<BusRoutingContextStore>();
                    services.AddSingleton<BusRouterControlContextStore>();

                    services.AddSingleton<IMapAccessor, MapStoreAccessor>();
                    services.AddSingleton<IBusRoutingContextAccessor, BusRoutingContextStoreAccessor>();
                    services.AddSingleton<IBusRouterControlContextAccessor, BusRouterControlContextStoreAccessor>();

                    services.AddSingleton<IBusRoutingProvider, BusRoutingProvider>();
                    services.AddSingleton<IBusService, BusService>();
                    services.AddSingleton<IStopService, StopService>();

                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<SetBusRoutingViewModel>();
                    services.AddSingleton<BusRouterViewModel>();

                    services.AddSingleton<NavigationStore>();

                    services.AddSingleton<Func<BusRouterViewModel>>((s) => () => s.GetRequiredService<BusRouterViewModel>());
                    services.AddSingleton<INavigationService<BusRouterViewModel>, NavigationService<BusRouterViewModel>>();
                    services.AddSingleton<Func<SetBusRoutingViewModel>>((s) => () => s.GetRequiredService<SetBusRoutingViewModel>());
                    services.AddSingleton<INavigationService<SetBusRoutingViewModel>, NavigationService<SetBusRoutingViewModel>>();


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