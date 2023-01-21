using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public partial class SpellDeckViewModel : ViewModelBase
    {

        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;

        #endregion

        #region Properties

        [ObservableProperty]
        private MagicSchool magicSchoolType;

        [ObservableProperty]
        private ICard selectedCard;

        [ObservableProperty]
        private string addedSpellDeckName;

        [ObservableProperty]
        private string addedSpellCardName;

        [ObservableProperty]
        private CharacterModel selectedCharacter;

        [ObservableProperty]
        private SpellDeckModel selectedSpellDeck;

        [ObservableProperty]
        private SpellCardModel selectedSpellCard;

        [ObservableProperty]
        private ObservableCollection<SpellCardModel> selectedSpellDeckCards;

        [ObservableProperty]
        private ObservableCollection<SpellCardModel> allSpellCards;

        [ObservableProperty]
        private ObservableCollection<SpellDeckModel> allSpellDecks;

        [ObservableProperty]
        private ObservableCollection<CharacterModel> allCharacters;

        #endregion Properties

        #region Actions, Events, Commands

        public ICommand DoNavigateCommand { get; }
        public ICommand CreateCharacterCommand { get; }
        public ICommand UpdateCharacterCommand { get; }
        public ICommand DeleteCharacterCommand { get; }

        public ICommand CreateSpellDeckCommand { get; }
        public ICommand UpdateSpellDeckCommand { get; }
        public ICommand DeleteSpellDeckCommand { get; }

        public ICommand CreateSpellCardCommand { get; }

        #endregion Actions, Events, Commands

        #region Constructor

        public SpellDeckViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = Regex.Replace(nameof(SpellDeckViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");

            _cardDesignerStore = cardDesignerStore;

            UpdateCharacterCommand = new AddDeckToCharacterCommand(this, cardDesignerStore);

            CreateSpellDeckCommand = new CreateSpellDeckCommand(this, cardDesignerStore);
            UpdateSpellDeckCommand = new AddCardToSpellDeckCommand(this, cardDesignerStore);
            DeleteSpellDeckCommand = new DeleteSpellDeckCommand(this, cardDesignerStore);

            _cardDesignerStore.CharacterCreated += OnCharacterCreated;
            _cardDesignerStore.CharacterDeleted += OnCharacterDeleted;
            _cardDesignerStore.SpellDeckCreated += OnSpellDeckCreated;
            _cardDesignerStore.SpellDeckDeleted += OnSpellDeckDeleted;
            _cardDesignerStore.SpellCardCreated += OnSpellCardCreated;
        }

        #endregion

        #region Private methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllCharacters = new(_cardDesignerStore.Characters);
            AllSpellCards = new(_cardDesignerStore.SpellCards);
            AllSpellDecks = new(_cardDesignerStore.SpellDecks);
        }

        private void OnCharacterCreated(CharacterModel character)
        {
            AllCharacters.Add(character);
            SelectedCharacter = character;
        }

        private void OnCharacterDeleted(CharacterModel character)
        {
            AllCharacters.Remove(SelectedCharacter);
            SelectedCharacter = AllCharacters.FirstOrDefault();
        }

        private void OnSpellDeckCreated(SpellDeckModel spellDeck)
        {
            AllSpellDecks.Add(spellDeck);
            SelectedSpellDeck = spellDeck;
        }

        private void OnSpellDeckDeleted(SpellDeckModel spellDeck)
        {
            AllSpellDecks.Remove(SelectedSpellDeck);
            SelectedSpellDeck = AllSpellDecks.FirstOrDefault();
        }

        private void OnSpellCardCreated(SpellCardModel spellCard)
        {
            AllSpellCards.Add(spellCard);
            SelectedSpellCard = spellCard;
        }

        #endregion

        #region Public methods

        public static SpellDeckViewModel LoadViewModel(CardDesignerStore cardDesignerStore)
        {
            SpellDeckViewModel viewModel = new(cardDesignerStore);
            viewModel.LoadData();

            return viewModel;
        }

        #endregion
    }
}
