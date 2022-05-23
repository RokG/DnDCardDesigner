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
                services.AddSingleton<NavigationService<CardCreatorViewModel>>();
                services.AddSingleton<NavigationService<CardDisplayViewModel>>();
            });

            return hostBuilder;
        }
    }
}