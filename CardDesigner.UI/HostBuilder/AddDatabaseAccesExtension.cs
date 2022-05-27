using CardDesigner.DataAccess.Services;
using CardDesigner.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                services.AddSingleton<ISpellDeckCreator, DatabaseSpellDeckCreator>();
                services.AddSingleton<ISpellDeckProvider, DatabaseSpellDeckProvider>();
            });

            return hostBuilder;
        }
    }
}