using CardDesigner.Domain.Services;
using CardDesigner.Domain.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CardDesigner.UI.HostBuilder
{
    public static class AddStoreExtension
    {
        public static IHostBuilder AddStores(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                // register stores
                services.AddSingleton<CardDesignerStore>();
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<SettingsStore>();
            });

            return hostBuilder;
        }
    }
}