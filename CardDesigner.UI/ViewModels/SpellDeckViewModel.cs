using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

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
        [NotifyCanExecuteChangedFor(nameof(CreateSpellDeckCommand))]
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
        private ObservableCollection<SpellCardModel> allSpellCards;

        [ObservableProperty]
        private ObservableCollection<SpellDeckModel> allSpellDecks;

        [ObservableProperty]
        private ObservableCollection<CharacterModel> allCharacters;

        #endregion Properties

        #region Constructor

        public SpellDeckViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = Regex.Replace(nameof(SpellDeckViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Create, view and edit Spell Decks";

            _cardDesignerStore = cardDesignerStore;

            _cardDesignerStore.CharacterCreated += OnCharacterCreated;
            _cardDesignerStore.CharacterDeleted += OnCharacterDeleted;
            _cardDesignerStore.SpellDeckCreated += OnSpellDeckCreated;
            _cardDesignerStore.SpellDeckDeleted += OnSpellDeckDeleted;
            _cardDesignerStore.SpellCardCreated += OnSpellCardCreated;
            _cardDesignerStore.SpellDeckUpdated += OnSpellDeckUpdated;

            // TODO: is this OK? how is it different from old method (before MVVM toolkit)
            LoadData();
        }

        private void OnSpellDeckUpdated(SpellDeckModel spellDeck)
        {
            SelectedSpellDeck = spellDeck;
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

        #region Commands

        [RelayCommand(CanExecute = nameof(CanCreateSpellDeck))]
        private async void CreateSpellDeck()
        {
            await _cardDesignerStore.CreateSpellDeck(new SpellDeckModel() { Name = AddedSpellDeckName });
        }

        private bool CanCreateSpellDeck()
        {
            bool noName = (AddedSpellDeckName == string.Empty || AddedSpellDeckName == null);
            bool spellDeckExists = AllSpellDecks == null ? false : AllSpellDecks.Where(c => c.Name == AddedSpellDeckName).Any();

            return (!noName && !spellDeckExists);
        }

        [RelayCommand]
        private async void UpdateCharacter()
        {
            SelectedCharacter.SpellDeck = SelectedSpellDeck;
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand]
        private async void AddCardToDeck()
        {
            SelectedSpellDeck.SpellCards.Add(SelectedSpellCard);
            await _cardDesignerStore.UpdateSpellDeck(SelectedSpellDeck);
        }

        [RelayCommand]
        private async void DeleteSpellDeck()
        {
            await _cardDesignerStore.DeleteSpellDeck(SelectedSpellDeck);
        }

        #endregion
    }
}
