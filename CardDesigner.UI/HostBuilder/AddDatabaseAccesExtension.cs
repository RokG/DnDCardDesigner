using CardDesigner.DataAccess.Services;
using CardDesigner.Domain.Services;
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
    public static class AddDatabaseAccesExtension
    {
        public static IHostBuilder AddDatabaseAccess(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                // register invoice services
                services.AddSingleton<ICharacterCreator, DatabaseCharacterCreator>();
                services.AddSingleton<ICharacterProvider, DatabaseCharacterProvider>();
                services.AddSingleton<ISpellCardCreator, DatabaseSpellCardCreator>();
                services.AddSingleton<ISpellCardProvider, DatabaseSpellCardProvider>();
            });

            return hostBuilder;
        }
    }
}