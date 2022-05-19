using CardDesigner.Domain.Models;
using CardDesigner.UI.ViewModels;
using InvoiceMe.Domain.Stores;

namespace CardDesigner.UI.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public NavigateCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object? parameter)
        {
            _navigationStore.CurrentViewModel = new CardDisplayViewModel(_navigationStore);
        }
    }
}