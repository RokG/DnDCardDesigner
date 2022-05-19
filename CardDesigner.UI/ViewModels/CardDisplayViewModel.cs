using CardDesigner.Domain.Models;
using InvoiceMe.Domain.Stores;
using System.ComponentModel;

namespace CardDesigner.UI.ViewModels
{
    public class CardDisplayViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public CardDisplayViewModel(NavigationStore navigationStore)
        {
            Name = nameof(CardDisplayViewModel).Replace("ViewModel", "");
            _navigationStore = navigationStore;
        }
    }
}