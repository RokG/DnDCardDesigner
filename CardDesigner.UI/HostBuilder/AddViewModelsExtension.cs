using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CardDesigner.UI.HostBuilder
{
    public static class AddViewModelsExtension
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddTransient((s) => CreateHomeViewModel(s));
                services.AddTransient((s) => CreateSpellCardViewModel(s));
                services.AddTransient((s) => CreateSpellDeckViewModel(s));
                services.AddTransient((s) => CreateItemCardViewModel(s));
                services.AddTransient((s) => CreateCharacterViewModel(s));

                services.AddSingleton<Func<HomeViewModel>>((s) => () => s.GetRequiredService<HomeViewModel>());
                services.AddSingleton<Func<SpellCardViewModel>>((s) => () => s.GetRequiredService<SpellCardViewModel>());
                services.AddSingleton<Func<SpellDeckViewModel>>((s) => () => s.GetRequiredService<SpellDeckViewModel>());
                services.AddSingleton<Func<ItemCardViewModel>>((s) => () => s.GetRequiredService<ItemCardViewModel>());
                services.AddSingleton<Func<CharacterViewModel>>((s) => () => s.GetRequiredService<CharacterViewModel>());

                services.AddSingleton<MainViewModel>();
            });

            return hostBuilder;
        }

        private static HomeViewModel CreateHomeViewModel(IServiceProvider s)
        {
            return HomeViewModel.LoadViewModel(s.GetRequiredService<CardDesignerStore>());
        }

        private static SpellCardViewModel CreateSpellCardViewModel(IServiceProvider s)
        {
            return SpellCardViewModel.LoadViewModel(s.GetRequiredService<CardDesignerStore>());
        }

        private static SpellDeckViewModel CreateSpellDeckViewModel(IServiceProvider s)
        {
            return SpellDeckViewModel.LoadViewModel(s.GetRequiredService<CardDesignerStore>());
        }

        private static ItemCardViewModel CreateItemCardViewModel(IServiceProvider s)
        {
            return ItemCardViewModel.LoadViewModel(s.GetRequiredService<CardDesignerStore>());
        }

        private static CharacterViewModel CreateCharacterViewModel(IServiceProvider s)
        {
            return CharacterViewModel.LoadViewModel(s.GetRequiredService<CardDesignerStore>());
        }
    }
}