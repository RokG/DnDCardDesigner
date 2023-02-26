using CardDesigner.Domain.Services;
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
                services.AddTransient((s) => CreateCardDecksViewModel(s));
                services.AddTransient((s) => CreateItemCardViewModel(s));
                services.AddTransient((s) => CreateCharacterCardViewModel(s));
                services.AddTransient((s) => CreateCharacterViewModel(s));
                services.AddTransient((s) => CreateMinionViewModel(s));
                services.AddTransient((s) => CreateMinionCardViewModel(s));
                services.AddTransient((s) => CreateDeckDesignViewModel(s));
                services.AddTransient((s) => CreatePrintLayoutViewModel(s));

                services.AddSingleton<Func<HomeViewModel>>((s) => () => s.GetRequiredService<HomeViewModel>());
                services.AddSingleton<Func<SpellCardViewModel>>((s) => () => s.GetRequiredService<SpellCardViewModel>());
                services.AddSingleton<Func<CardDecksViewModel>>((s) => () => s.GetRequiredService<CardDecksViewModel>());
                services.AddSingleton<Func<ItemCardViewModel>>((s) => () => s.GetRequiredService<ItemCardViewModel>());
                services.AddSingleton<Func<CharacterCardViewModel>>((s) => () => s.GetRequiredService<CharacterCardViewModel>());
                services.AddSingleton<Func<CharacterViewModel>>((s) => () => s.GetRequiredService<CharacterViewModel>());
                services.AddSingleton<Func<MinionViewModel>>((s) => () => s.GetRequiredService<MinionViewModel>());
                services.AddSingleton<Func<MinionCardViewModel>>((s) => () => s.GetRequiredService<MinionCardViewModel>());
                services.AddSingleton<Func<DeckDesignViewModel>>((s) => () => s.GetRequiredService<DeckDesignViewModel>());
                services.AddSingleton<Func<PrintLayoutViewModel>>((s) => () => s.GetRequiredService<PrintLayoutViewModel>());

                services.AddSingleton<MainViewModel>();
            });

            return hostBuilder;
        }

        private static HomeViewModel CreateHomeViewModel(IServiceProvider s)
        {
            return HomeViewModel.LoadViewModel(s.GetRequiredService<CardDesignerStore>(), s.GetRequiredService<NavigationStore>(), s.GetRequiredService<SettingsStore>());
        }

        private static SpellCardViewModel CreateSpellCardViewModel(IServiceProvider s)
        {
            return SpellCardViewModel.LoadViewModel(s.GetRequiredService<CardDesignerStore>(), s.GetRequiredService<NavigationStore>(), s.GetRequiredService<SettingsStore>());
        }

        private static CardDecksViewModel CreateCardDecksViewModel(IServiceProvider s)
        {
            return CardDecksViewModel.LoadViewModel(s.GetRequiredService<CardDesignerStore>(), s.GetRequiredService<NavigationStore>(), s.GetRequiredService<SettingsStore>());
        }

        private static ItemCardViewModel CreateItemCardViewModel(IServiceProvider s)
        {
            return ItemCardViewModel.LoadViewModel(s.GetRequiredService<CardDesignerStore>(), s.GetRequiredService<NavigationStore>(), s.GetRequiredService<SettingsStore>());
        }

        private static CharacterCardViewModel CreateCharacterCardViewModel(IServiceProvider s)
        {
            return CharacterCardViewModel.LoadViewModel(s.GetRequiredService<CardDesignerStore>(), s.GetRequiredService<NavigationStore>(), s.GetRequiredService<SettingsStore>());
        }

        private static CharacterViewModel CreateCharacterViewModel(IServiceProvider s)
        {
            return CharacterViewModel.LoadViewModel(s.GetRequiredService<CardDesignerStore>(), s.GetRequiredService<NavigationStore>(), s.GetRequiredService<SettingsStore>());
        }

        private static MinionViewModel CreateMinionViewModel(IServiceProvider s)
        {
            return MinionViewModel.LoadViewModel(s.GetRequiredService<CardDesignerStore>(), s.GetRequiredService<NavigationStore>(), s.GetRequiredService<SettingsStore>());
        }

        private static MinionCardViewModel CreateMinionCardViewModel(IServiceProvider s)
        {
            return MinionCardViewModel.LoadViewModel(s.GetRequiredService<CardDesignerStore>(), s.GetRequiredService<NavigationStore>(), s.GetRequiredService<SettingsStore>());
        }

        private static DeckDesignViewModel CreateDeckDesignViewModel(IServiceProvider s)
        {
            return DeckDesignViewModel.LoadViewModel(s.GetRequiredService<CardDesignerStore>(), s.GetRequiredService<NavigationStore>(), s.GetRequiredService<SettingsStore>());
        }

        private static PrintLayoutViewModel CreatePrintLayoutViewModel(IServiceProvider s)
        {
            return PrintLayoutViewModel.LoadViewModel(s.GetRequiredService<CardDesignerStore>(), s.GetRequiredService<NavigationStore>(), s.GetRequiredService<SettingsStore>());
        }
    }
}