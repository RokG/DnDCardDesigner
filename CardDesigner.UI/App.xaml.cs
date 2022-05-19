using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.UI.ViewModels;
using InvoiceMe.Domain.Stores;
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
            _navigationStore.CurrentViewModel = new CardCreatorViewModel(_navigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}