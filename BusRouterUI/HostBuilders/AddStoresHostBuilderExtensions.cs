using BusRouterUI.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BusRouterUI.HostBuilders
{
    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<MapStore>();
                services.AddSingleton<BusRoutingContextStore>();
                services.AddSingleton<BusRouterControlContextStore>();
            });

            return hostBuilder;
        }
    }
}