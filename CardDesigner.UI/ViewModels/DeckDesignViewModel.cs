using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Enums;
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
    public partial class DeckDesignViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;
        private readonly NavigationStore _navigationStore;

        #endregion

        #region Properties

        #region Character

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AssignItemDeckDesignCommand))]
        [NotifyCanExecuteChangedFor(nameof(AssignSpellDeckDesignCommand))]
        [NotifyCanExecuteChangedFor(nameof(AssignDeckBackgroundDesignCommand))]
        private CharacterModel selectedCharacter;

        [ObservableProperty]
        private ObservableCollection<CharacterModel> allCharacters;

        [ObservableProperty]
        private ObservableCollection<SpellDeckModel> characterSpellDecks;

        [ObservableProperty]
        private ObservableCollection<ItemDeckModel> characterItemDecks;

        #endregion

        #region DeckBackground

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateDeckBackgroundDesignCommand))]
        private string addedDeckBackgroundDesignName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AssignDeckBackgroundDesignCommand))]
        private DeckBackgroundDesignModel selectedBackgroundDesign;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AssignDeckBackgroundDesignCommand))]
        private DeckBackgroundDesignModel selectedCharacterBackgroundDesign;

        [ObservableProperty]
        private ObservableCollection<DeckBackgroundDesignModel> allDeckBackgroundDesigns;

        #endregion

        #region ItemDecks

        [ObservableProperty]
        private ItemCardModel testItemCard;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateItemDeckDesignCommand))]
        private string addedItemDeckDesignName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AssignItemDeckDesignCommand))]
        private ItemDeckDesignModel selectedItemDeckDesign;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AssignItemDeckDesignCommand))]
        private ItemDeckModel selectedItemDeck;

        [ObservableProperty]
        private ObservableCollection<ItemDeckDesignModel> allItemDeckDesigns;

        [ObservableProperty]
        private ObservableCollection<ItemDeckModel> allItemDecks;

        #endregion

        #region SpellDecks

        [ObservableProperty]
        private SpellCardModel testSpellCard;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateSpellDeckDesignCommand))]
        private string addedSpellDeckDesignName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AssignSpellDeckDesignCommand))]
        private SpellDeckDesignModel selectedSpellDeckDesign;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AssignSpellDeckDesignCommand))]
        private SpellDeckModel selectedSpellDeck;

        [ObservableProperty]
        private ObservableCollection<SpellDeckDesignModel> allSpellDeckDesigns;

        [ObservableProperty]
        private ObservableCollection<SpellDeckModel> allSpellDecks;

        #endregion

        #endregion

        #region Actions, Events, Commands

        public ICommand DoNavigateCommand { get; }

        #endregion Actions, Events, Commands

        #region Constructor

        public DeckDesignViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            Name = Regex.Replace(nameof(DeckDesignViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Create, view and edit Card designs";
            Type = ViewModelType.DeckDesigner;

            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;

            _cardDesignerStore.CharacterChanged += OnCharacterChanged;
            _cardDesignerStore.SpellDeckDesignChanged += OnSpellDeckDesignChanged;
            _cardDesignerStore.ItemDeckDesignChanged += OnItemDeckDesignChanged;
            _cardDesignerStore.CharacterDeckDesignChanged += OnCharacterDeckDesignChanged;

            _navigationStore.CurrentViewModelChanged += OnNavigatingAway;

            LoadData();

            SelectedSpellDeckDesign = AllSpellDeckDesigns.FirstOrDefault();
            SelectedItemDeckDesign = AllItemDeckDesigns.FirstOrDefault();
            selectedCharacterBackgroundDesign = AllDeckBackgroundDesigns.FirstOrDefault();
            SelectedCharacter = AllCharacters.FirstOrDefault();
            SelectedItemDeck = AllItemDecks.FirstOrDefault();

            GetCharacterSpellDecks();
            GetCharacterItemDecks();
            GetCharacterBackgroundDeck();
            UpdateSpellDeckDesign();
            UpdateItemDeckDesign();

            // Without this, the selected character in list on UI does not update?
            SelectedCharacter = AllCharacters.FirstOrDefault();
        }

        private void OnNavigatingAway()
        {
            _navigationStore.SelectedItemDeck = SelectedItemDeck;
            _navigationStore.SelectedSpellDeck = SelectedSpellDeck;
            _navigationStore.SelectedItemDeckDesign = SelectedItemDeckDesign;
            _navigationStore.CurrentViewModelChanged -= OnNavigatingAway;
        }

        #endregion

        #region Private methods

        private void OnItemDeckDesignChanged(ItemDeckDesignModel itemDeckDesign, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllItemDeckDesigns.Remove(itemDeckDesign);
                    SelectedItemDeckDesign = AllItemDeckDesigns.FirstOrDefault();
                    break;
                case DataChangeType.Removed:
                    break;
                case DataChangeType.Updated:
                    break;
                case DataChangeType.Deleted:
                    AllItemDeckDesigns.Add(itemDeckDesign);
                    SelectedItemDeckDesign = itemDeckDesign;
                    break;
                default:
                    break;
            }
        }

        private void OnCharacterDeckDesignChanged(DeckBackgroundDesignModel characterDeckDesign, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllDeckBackgroundDesigns.Add(characterDeckDesign);
                    SelectedBackgroundDesign = characterDeckDesign;
                    break;
                case DataChangeType.Removed:
                    break;
                case DataChangeType.Updated:
                    break;
                case DataChangeType.Deleted:
                    AllDeckBackgroundDesigns.Remove(characterDeckDesign);
                    SelectedBackgroundDesign = AllDeckBackgroundDesigns.FirstOrDefault();
                    break;
                default:
                    break;
            }
        }

        private void OnSpellDeckDesignChanged(SpellDeckDesignModel spellDeckDesign, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllSpellDeckDesigns.Add(spellDeckDesign);
                    SelectedSpellDeckDesign = spellDeckDesign;
                    break;
                case DataChangeType.Removed:
                    break;
                case DataChangeType.Updated:
                    break;
                case DataChangeType.Deleted:
                    AllSpellDeckDesigns.Remove(spellDeckDesign);
                    SelectedSpellDeckDesign = AllSpellDeckDesigns.FirstOrDefault();
                    break;
                default:
                    break;
            }
        }

        private void OnCharacterChanged(CharacterModel character, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllCharacters.Add(character);
                    SelectedCharacter = character;
                    break;
                case DataChangeType.Updated:
                    SelectedCharacter = character;
                    GetCharacterSpellDecks();
                    GetCharacterItemDecks();
                    break;
                case DataChangeType.Deleted:
                    AllCharacters.Remove(SelectedCharacter);
                    SelectedCharacter = AllCharacters.FirstOrDefault();
                    break;
                default:
                    break;
            }
        }

        private void GetCharacterSpellDecks()
        {
            CharacterSpellDecks = new();
            if (SelectedCharacter?.SpellDeckDescriptors != null)
            {
                foreach (SpellDeckDesignLinkerModel deckDescriptor in SelectedCharacter.SpellDeckDescriptors)
                {
                    CharacterSpellDecks.Add(AllSpellDecks.First(i => i.ID == deckDescriptor.SpellDeckID));
                }
            }
            SelectedSpellDeck = CharacterSpellDecks.FirstOrDefault();
        }

        private void GetCharacterItemDecks()
        {
            CharacterItemDecks = new();
            if (SelectedCharacter?.ItemDeckDescriptors != null)
            {
                foreach (ItemDeckDesignLinkerModel deckDescriptor in SelectedCharacter.ItemDeckDescriptors)
                {
                    CharacterItemDecks.Add(AllItemDecks.First(i => i.ID == deckDescriptor.ItemDeckID));
                }
            }
            SelectedItemDeck = CharacterItemDecks.FirstOrDefault();
        }

        #endregion

        #region Public methods

        public static DeckDesignViewModel LoadViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            DeckDesignViewModel viewModel = new(cardDesignerStore, navigationStore);

            viewModel.LoadData();

            return viewModel;
        }

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllCharacters = new(_cardDesignerStore.Characters);
            AllSpellDecks = new(_cardDesignerStore.SpellDecks);
            AllItemDecks = new(_cardDesignerStore.ItemDecks);
            AllSpellDeckDesigns = new(_cardDesignerStore.SpellDeckDesigns);
            AllItemDeckDesigns = new(_cardDesignerStore.ItemDeckDesigns);
            AllDeckBackgroundDesigns = new(_cardDesignerStore.DeckBackgroundDesigns);
        }

        #endregion

        #region Commands

        [RelayCommand(CanExecute = nameof(CanCreateSpellDeckDesign))]
        private async void CreateSpellDeckDesign()
        {
            await _cardDesignerStore.CreateCardDesign(new SpellDeckDesignModel() { Name = AddedSpellDeckDesignName });
        }

        private bool CanCreateSpellDeckDesign()
        {
            bool noName = (AddedSpellDeckDesignName == string.Empty || AddedSpellDeckDesignName == null);

            return !noName;
        }


        [RelayCommand(CanExecute = nameof(CanCreateItemDeckDesign))]
        private async void CreateItemDeckDesign()
        {
            await _cardDesignerStore.CreateCardDesign(new ItemDeckDesignModel() { Name = AddedItemDeckDesignName });
        }

        private bool CanCreateItemDeckDesign()
        {
            bool noName = (AddedItemDeckDesignName == string.Empty || AddedItemDeckDesignName == null);

            return !noName;
        }


        [RelayCommand(CanExecute = nameof(CanCreateDeckBackgroundDesign))]
        private async void CreateDeckBackgroundDesign()
        {
            await _cardDesignerStore.CreateCardDesign(new DeckBackgroundDesignModel() { Name = AddedDeckBackgroundDesignName });
        }

        private bool CanCreateDeckBackgroundDesign()
        {
            bool noName = (AddedDeckBackgroundDesignName == string.Empty || AddedDeckBackgroundDesignName == null);

            return !noName;
        }

        [RelayCommand]
        private async void UpdateSpellDeckDesign()
        {
            await _cardDesignerStore.UpdateCardDesign(SelectedSpellDeckDesign);
        }

        [RelayCommand]
        private async void UpdateItemDeckDesign()
        {
            await _cardDesignerStore.UpdateCardDesign(SelectedItemDeckDesign);
        }

        [RelayCommand]
        private async void UpdateDeckBackgroundDesign()
        {
            await _cardDesignerStore.UpdateCardDesign(SelectedBackgroundDesign);
        }

        [RelayCommand]
        private async void DeleteSpellDeckDesign()
        {
            await _cardDesignerStore.DeleteCardDesign(SelectedSpellDeckDesign);
        }

        [RelayCommand]
        private async void DeleteItemDeckDesign()
        {
            await _cardDesignerStore.DeleteCardDesign(SelectedItemDeckDesign);
        }

        [RelayCommand]
        private async void DeleteDeckBackgroundDesign()
        {
            await _cardDesignerStore.DeleteCardDesign(SelectedBackgroundDesign);
        }

        [RelayCommand(CanExecute = nameof(CanAssignSpellDeckDesign))]
        private async void AssignSpellDeckDesign()
        {
            await _cardDesignerStore.UpdateCardDesign(SelectedSpellDeckDesign);
            if (SelectedSpellDeckDesign != null)
            {
                SelectedCharacter.SpellDeckDescriptors.FirstOrDefault(c => c.SpellDeckID == SelectedSpellDeck.ID).DesignID = SelectedSpellDeckDesign.ID;
            }
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        private bool CanAssignSpellDeckDesign()
        {
            return SelectedCharacter != null && SelectedSpellDeckDesign != null && SelectedSpellDeck != null;
        }

        [RelayCommand(CanExecute = nameof(CanAssignItemDeckDesign))]
        private async void AssignItemDeckDesign()
        {
            await _cardDesignerStore.UpdateCardDesign(SelectedItemDeckDesign);
            if (SelectedItemDeckDesign != null)
            {
                SelectedCharacter.ItemDeckDescriptors.FirstOrDefault(c => c.ItemDeckID == SelectedItemDeck.ID).DesignID = SelectedItemDeckDesign.ID;
            }
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }
        private bool CanAssignItemDeckDesign()
        {
            return SelectedCharacter != null && SelectedItemDeckDesign != null && SelectedItemDeck != null;
        }

        [RelayCommand(CanExecute = nameof(CanAssignDeckBackgroundDesign))]
        private async void AssignDeckBackgroundDesign()
        {
            await _cardDesignerStore.UpdateCardDesign(SelectedBackgroundDesign);
            if (SelectedBackgroundDesign != null)
            {
                SelectedCharacter.DeckBackgroundDesign = SelectedBackgroundDesign;
            }
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }
        private bool CanAssignDeckBackgroundDesign()
        {
            return SelectedCharacter != null && SelectedBackgroundDesign != null;
        }

        partial void OnSelectedCharacterChanged(CharacterModel characterModel)
        {
            GetCharacterSpellDecks();
            GetCharacterItemDecks();
            GetCharacterBackgroundDeck();
        }

        private void GetCharacterBackgroundDeck()
        {
            SelectedCharacterBackgroundDesign = SelectedCharacter?.DeckBackgroundDesign != null
                ? AllDeckBackgroundDesigns.FirstOrDefault(dd => dd.ID == SelectedCharacter.DeckBackgroundDesign.ID)
                : AllDeckBackgroundDesigns.FirstOrDefault();
            SelectedBackgroundDesign = SelectedCharacterBackgroundDesign;
        }

        #endregion
    }
}
