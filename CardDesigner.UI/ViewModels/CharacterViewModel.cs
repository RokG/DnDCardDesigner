using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using System.Collections.Generic;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public class CharacterViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;

        #endregion

        #region Properties

        private CharacterModel _selectedCharacter;
        public CharacterModel SelectedCharacter
        {
            get => _selectedCharacter;
            set
            {
                SetProperty(ref _selectedCharacter, value);
                if (SelectedCharacter.SpellDeck != null)
                {
                    SelectedSpellDeck = new List<SpellDeckModel>();
                    SelectedSpellDeck.Add(SelectedCharacter.SpellDeck);
                    SelectedSpellDeckCards = new List<SpellCardModel>(SelectedCharacter.SpellDeck.SpellCards);
                }
            }
        }

        private List<SpellDeckModel> _selectedSpellDeck;
        public List<SpellDeckModel> SelectedSpellDeck
        {
            get => _selectedSpellDeck;
            set => SetProperty(ref _selectedSpellDeck, value);
        }

        private List<SpellCardModel> _selectedSpellDeckCards;
        public List<SpellCardModel> SelectedSpellDeckCards
        {
            get => _selectedSpellDeckCards;
            set => SetProperty(ref _selectedSpellDeckCards, value);
        }

        private List<SpellCardModel> _allSpellCards;
        public List<SpellCardModel> AllSpellCards
        {
            get => _allSpellCards;
            set => SetProperty(ref _allSpellCards, value);
        }

        private List<SpellDeckModel> _allSpellDecks;
        public List<SpellDeckModel> AllSpellDecks
        {
            get => _allSpellDecks;
            set => SetProperty(ref _allSpellDecks, value);
        }

        private List<CharacterModel> _characters;
        public List<CharacterModel> AllCharacters
        {
            get => _characters;
            set => SetProperty(ref _characters, value);
        }

        #endregion

        #region Actions, Events, Commands

        public ICommand DoNavigateCommand { get; }

        #endregion Actions, Events, Commands

        #region Constructor

        public CharacterViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = nameof(CharacterViewModel).Replace("ViewModel", "");

            _cardDesignerStore = cardDesignerStore;
        }

        #endregion

        #region Private methods

        #endregion

        #region Public methods

        public static CharacterViewModel LoadViewModel(CardDesignerStore cardDesignerStore)
        {
            CharacterViewModel viewModel = new(cardDesignerStore);
            viewModel.LoadData();

            return viewModel;
        }

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllCharacters = new(_cardDesignerStore.Characters);
            AllSpellCards = new(_cardDesignerStore.SpellCards);
            AllSpellDecks = new(_cardDesignerStore.SpellDecks);
        }

        #endregion
    }
}
