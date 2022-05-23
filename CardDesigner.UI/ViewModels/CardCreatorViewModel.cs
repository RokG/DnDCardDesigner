using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.Commands;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public class CardCreatorViewModel : ViewModelBase
    {
        #region Fields

        private readonly CharacterModel _character;

        #endregion Fields

        #region Properties

        private CardDeckModel? _selectedDeck;

        public CardDeckModel? SelectedDeck
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

        public CardCreatorViewModel(CharacterModel character, NavigationService navigationService)
        {
            Name = nameof(CardCreatorViewModel).Replace("ViewModel", "");

            _character = character;

            AddCardCommand = new AddCardCommand(this, character, navigationService);
            DoNavigateCommand = new NavigateCommand(navigationService);

            SelectedDeck = new CardDeckModel(DeckType.Spells.ToString(), DeckType.Spells);
            SelectedCard = new SpellCardModel() { Name = "blabla" };
        }
    }
}