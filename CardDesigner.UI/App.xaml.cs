using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System;
using System.Windows;

namespace CardDesigner.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Character _character;
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _character = new Character("Gimble Locklen");
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
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