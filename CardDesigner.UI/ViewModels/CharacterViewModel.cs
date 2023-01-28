using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public partial class CharacterViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;

        #endregion

        #region Properties
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCharacterCommand))]
        private string addedCharacterName;

        [ObservableProperty]
        private string addedSpellDeckName;

        [ObservableProperty]
        private string addedItemDeckName;


        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AssignItemDeckToCharacterCommand))]
        [NotifyCanExecuteChangedFor(nameof(AssignSpellDeckToCharacterCommand))]
        private CharacterModel selectedCharacter;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AssignSpellDeckToCharacterCommand))]
        private SpellDeckModel selectedSpellDeck;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AssignItemDeckToCharacterCommand))]
        private ItemDeckModel selectedItemDeck;


        [ObservableProperty]
        private ObservableCollection<SpellCardModel> selectedSpellDeckCards;

        [ObservableProperty]
        private ObservableCollection<SpellDeckModel> allSpellDecks;

        [ObservableProperty]
        private ObservableCollection<ItemDeckModel> allItemDecks;

        [ObservableProperty]
        private ObservableCollection<CharacterModel> allCharacters;

        #endregion

        #region Actions, Events, Commands

        public ICommand DoNavigateCommand { get; }

        #endregion Actions, Events, Commands

        #region Constructor

        public CharacterViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = Regex.Replace(nameof(CharacterViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Create, view and edit Characters";

            _cardDesignerStore = cardDesignerStore;

            _cardDesignerStore.CharacterCreated += OnCharacterCreated;
            _cardDesignerStore.CharacterDeleted += OnCharacterDeleted;

            LoadData();
        }

        #endregion

        #region Private methods
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
            AllSpellDecks = new(_cardDesignerStore.SpellDecks);
            AllItemDecks = new(_cardDesignerStore.ItemDecks);
        }

        #endregion

        #region Commands

        [RelayCommand(CanExecute = nameof(CanCreateCharacter))]
        private async void CreateCharacter()
        {
            await _cardDesignerStore.CreateCharacter(new CharacterModel() { Name = AddedCharacterName });
        }

        private bool CanCreateCharacter()
        {
            bool noName = (AddedCharacterName == string.Empty || AddedCharacterName == null);
            bool spellDeckExists = AllCharacters == null ? false : AllCharacters.Where(c => c.Name == AddedCharacterName).Any();

            return (!noName && !spellDeckExists);
        }

        [RelayCommand]
        private async void DeleteCharacter()
        {
            await _cardDesignerStore.DeleteCharacter(SelectedCharacter);
        }

        [RelayCommand(CanExecute = nameof(CanAssignSpellDeckToCharacter))]
        private async void AssignSpellDeckToCharacter(SpellDeckModel spellDeck)
        {
            SelectedCharacter.SpellDecks.Add(spellDeck);
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand(CanExecute = nameof(CanAssignItemDeckToCharacter))]
        private async void AssignItemDeckToCharacter(ItemDeckModel itemDeck)
        {
            SelectedCharacter.ItemDecks.Add(itemDeck);
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        private bool CanAssignItemDeckToCharacter()
        {
            return SelectedItemDeck != null && SelectedCharacter != null;
        }
        private bool CanAssignSpellDeckToCharacter()
        {
            return SelectedSpellDeck != null && SelectedCharacter != null;
        }

        #endregion
    }
}
