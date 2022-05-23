using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesigner.UI.HostBuilder
{
    public static class AddViewModelsExtension
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                // register view models
                services.AddTransient((s) => CreateCardCreatorViewModel(s));
                services.AddTransient((s) => CreateCardDisplayViewModel(s));

                // register view model return func's
                services.AddSingleton<Func<CardCreatorViewModel>>((s) => () => s.GetRequiredService<CardCreatorViewModel>());
                services.AddSingleton<Func<CardDisplayViewModel>>((s) => () => s.GetRequiredService<CardDisplayViewModel>());

                services.AddSingleton<MainViewModel>();
            });

            return hostBuilder;
        }

        private static CardCreatorViewModel CreateCardCreatorViewModel(IServiceProvider s)
        {
            return CardCreatorViewModel.LoadViewModel(s.GetRequiredService<CardDesignerStore>());
        }

        private static CardDisplayViewModel CreateCardDisplayViewModel(IServiceProvider s)
        {
            return CardDisplayViewModel.LoadViewModel(s.GetRequiredService<CardDesignerStore>());
        }
    }
}