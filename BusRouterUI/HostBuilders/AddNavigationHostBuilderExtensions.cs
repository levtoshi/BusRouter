using BusRouterUI.Navigation.Services;
using BusRouterUI.Navigation.Stores;
using BusRouterUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BusRouterUI.HostBuilders
{
    public static class AddNavigationHostBuilderExtensions
    {
        public static IHostBuilder AddNavigation(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<NavigationStore>();

                services.AddSingleton<Func<BusRouterViewModel>>((s) => () => s.GetRequiredService<BusRouterViewModel>());
                services.AddSingleton<INavigationService<BusRouterViewModel>, NavigationService<BusRouterViewModel>>();
                services.AddSingleton<Func<SetBusRoutingViewModel>>((s) => () => s.GetRequiredService<SetBusRoutingViewModel>());
                services.AddSingleton<INavigationService<SetBusRoutingViewModel>, NavigationService<SetBusRoutingViewModel>>();
            });

            return hostBuilder;
        }
    }
}