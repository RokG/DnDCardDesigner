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
                services.AddSingleton<ICharacterService, DatabaseCharacterService>();
                services.AddSingleton<ICardDesignService, DatabaseCardDesignService>();
                services.AddSingleton<ISpellDeckService, DatabaseSpellDeckService>();
                services.AddSingleton<ISpellCardService, DatabaseSpellCardService>();
                services.AddSingleton<IItemDeckService, DatabaseItemDeckService>();
                services.AddSingleton<IItemCardService, DatabaseItemCardService>();
                services.AddSingleton<IJsonFileItemService, JsonFileItemsService>();
            });

            return hostBuilder;
        }
    }
}