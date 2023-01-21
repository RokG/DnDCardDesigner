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
                services.AddSingleton<ICharacterCreator, DatabaseCharacterCreator>();
                services.AddSingleton<ICharacterUpdater, DatabaseCharacterUpdater>();
                services.AddSingleton<ICharacterDeleter, DatabaseCharacterDeleter>();
                services.AddSingleton<ICharacterProvider, DatabaseCharacterProvider>();

                services.AddSingleton<ISpellDeckCreator, DatabaseSpellDeckCreator>();
                services.AddSingleton<ISpellDeckProvider, DatabaseSpellDeckProvider>();
                services.AddSingleton<ISpellDeckUpdater, DatabaseSpellDeckUpdater>();
                services.AddSingleton<ISpellDeckDeleter, DatabaseSpellDeckDeleter>();

                services.AddSingleton<ISpellCardCreator, DatabaseSpellCardCreator>();
                services.AddSingleton<ISpellCardUpdater, DatabaseSpellCardUpdater>();
                services.AddSingleton<ISpellCardDeleter, DatabaseSpellCardDeleter>();
                services.AddSingleton<ISpellCardProvider, DatabaseSpellCardProvider>();

                services.AddSingleton<IItemDeckCreator, DatabaseItemDeckCreator>();
                services.AddSingleton<IItemDeckProvider,DatabaseItemDeckProvider>();
                services.AddSingleton<IItemDeckUpdater, DatabaseItemDeckUpdater>();
                services.AddSingleton<IItemDeckDeleter, DatabaseItemDeckDeleter>();

                services.AddSingleton<IItemCardCreator, DatabaseItemCardCreator>();
                services.AddSingleton<IItemCardUpdater, DatabaseItemCardUpdater>();
                services.AddSingleton<IItemCardDeleter, DatabaseItemCardDeleter>();
                services.AddSingleton<IItemCardProvider,DatabaseItemCardProvider>();
            });

            return hostBuilder;
        }
    }
}