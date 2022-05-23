using CardDesigner.Domain.Models;
using CardDesigner.UI.Commands;
using InvoiceMe.Domain.Stores;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public class CardDisplayViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Character _character;

        #region Actions, Events, Commands

        public ICommand DoNavigateCommand { get; }

        #endregion Actions, Events, Commands

        public CardDisplayViewModel(Character character, NavigationStore navigationStore, Func<CardCreatorViewModel> createViewModel)
        {
            Name = nameof(CardDisplayViewModel).Replace("ViewModel", "");

            _navigationStore = navigationStore;
            _character = character;

            DoNavigateCommand = new NavigateCommand(navigationStore, createViewModel);
        }
    }
}