using BLL.InterfaceAccessors;
using BusRouterUI.Accessors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BusRouterUI.HostBuilders
{
    public static class AddAccessorsHostBuilderExtensions
    {
        public static IHostBuilder AddAccessors(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IMapAccessor, MapStoreAccessor>();
                services.AddSingleton<IBusRoutingContextAccessor, BusRoutingContextStoreAccessor>();
                services.AddSingleton<IBusRouterControlContextAccessor, BusRouterControlContextStoreAccessor>();
            });

            return hostBuilder;
        }
    }
}