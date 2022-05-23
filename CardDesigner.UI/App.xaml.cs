using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Mapper;
using CardDesigner.Domain.Services;
using CardDesigner.UI.HostBuilder;
using CardDesigner.UI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace CardDesigner.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host
                .CreateDefaultBuilder()
                .AddNavigationServices()
                .AddDatabaseAccess()
                .AddViewModels()
                .AddStores()
                .ConfigureServices((hostContext, services) =>
                {
                    // Have to change properties of appSettings.json to Copy if newer (RMB on .json => Properties)
                    string connectionString = hostContext.Configuration.GetConnectionString("Default");

                    // register mapper
                    services.AddSingleton(CardDesignerMapper.CreateMapper());

                    // register db context
                    services.AddSingleton((s) => new CardDesignerDbContextFactory(connectionString, s.GetRequiredService<IMapper>()));

                    // register main view
                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });
                }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            // initialize db
            CardDesignerDbContextFactory invoiceMeDbContextFactory = _host.Services.GetRequiredService<CardDesignerDbContextFactory>();

            using CardDesignerDbContext dbContext = invoiceMeDbContextFactory.CreateDbContext();
            dbContext.Database.Migrate();

            // Navigate to home view
            _host.Services.GetRequiredService<NavigationService<CardCreatorViewModel>>().Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}