using BLL.Services.BusRoutingProviders;
using BLL.Services.BusServices;
using BLL.Services.StopServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BusRouterUI.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IBusRoutingProvider, BusRoutingProvider>();
                services.AddSingleton<IBusService, BusService>();
                services.AddSingleton<IStopService, StopService>();
            });

            return hostBuilder;
        }
    }
}