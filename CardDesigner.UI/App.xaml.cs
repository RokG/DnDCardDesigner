using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Mapper;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace CardDesigner.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=carddesign.db";
        private readonly CharacterModel _character;
        private readonly NavigationStore _navigationStore;
        private readonly IMapper _mapper;

        public App()
        {
            _mapper = CardDesignerMapper.CreateMapper();
            _character = new CharacterModel("Gimble Locklen");
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var options = new DbContextOptionsBuilder().UseSqlite(CONNECTION_STRING).Options;
            using (CardDesignerDbContext dbContext = new CardDesignerDbContext(options, _mapper))
            {
                // Uncomment this to test character creation
                //Character createdClient = dbContext.Characters.Add(new Character() { ID = 0, Name = "gorge" }).Entity;
                //dbContext.SaveChangesAsync();

                dbContext.Database.Migrate();
            }

            _navigationStore.CurrentViewModel = CreateCardCreatorViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();
            base.OnStartup(e);
        }

        private CardDisplayViewModel CreateCardDisplayViewModel()
        {
            return new CardDisplayViewModel(_character, new NavigationService(_navigationStore, CreateCardCreatorViewModel));
        }

        private CardCreatorViewModel CreateCardCreatorViewModel()
        {
            return new CardCreatorViewModel(_character, new NavigationService(_navigationStore, CreateCardDisplayViewModel));
        }
    }
}