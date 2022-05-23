using CardDesigner.Domain.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            });

            return hostBuilder;
        }
    }
}