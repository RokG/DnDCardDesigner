using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.Commands;
using InvoiceMe.Domain.Stores;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public class CardCreatorViewModel : ViewModelBase
    {
        #region Fields

        private readonly NavigationStore _navigationStore;
        private readonly Character _character;

        #endregion Fields

        #region Properties

        private CardDeck? _selectedDeck;

        public CardDeck? SelectedDeck
        {
            get => _selectedDeck;
            set => SetProperty(ref _selectedDeck, value);
        }

        private DeckType _desiredType;

        public DeckType DesiredType
        {
            get => _desiredType;
            set => SetProperty(ref _desiredType, value);
        }

        private ICard? _selectedCard;

        public ICard? SelectedCard
        {
            get => _selectedCard;
            set => SetProperty(ref _selectedCard, value);
        }

        #endregion Properties

        #region Actions, Events, Commands

        public ICommand AddCardCommand { get; }
        public ICommand DoNavigateCommand { get; }

        #endregion Actions, Events, Commands

        public CardCreatorViewModel(Character character, NavigationStore navigationStore, Func<CardDisplayViewModel> createViewModel)
        {
            Name = nameof(CardCreatorViewModel).Replace("ViewModel", "");

            _character = character;
            _navigationStore = navigationStore;

            AddCardCommand = new AddCardCommand(this, character);
            DoNavigateCommand = new NavigateCommand(navigationStore, createViewModel);

            SelectedDeck = new CardDeck(DeckType.Spells.ToString(), DeckType.Spells);
            SelectedCard = new SpellCard() { Name = "blabla" };
        }
    }
}