﻿using CardDesigner.Domain.Services;
using CardDesigner.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CardDesigner.UI.HostBuilder
{
    public static class AddNavigationServicesExtension
    {
        public static IHostBuilder AddNavigationServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                // registrer navigation services
                services.AddSingleton<NavigationService<HomeViewModel>>();
                services.AddSingleton<NavigationService<CharacterViewModel>>();
                services.AddSingleton<NavigationService<DeckDesignViewModel>>();
                services.AddSingleton<NavigationService<SpellCardViewModel>>();
                services.AddSingleton<NavigationService<CardDecksViewModel>>();
                services.AddSingleton<NavigationService<ItemCardViewModel>>();
                services.AddSingleton<NavigationService<CharacterCardViewModel>>();
            });

            return hostBuilder;
        }
    }
}