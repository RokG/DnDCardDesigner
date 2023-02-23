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
    public partial class DeckDesignViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;
        private readonly NavigationStore _navigationStore;

        #endregion

        #region Properties

        [ObservableProperty]
        private int selectedTabItem = 0;

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

        [ObservableProperty]
        private ObservableCollection<CharacterDeckModel> characterCharacterDecks;

        #endregion

        #region DeckBackground

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateDeckBackgroundDesignCommand))]
        private string addedDeckBackgroundDesignName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AssignDeckBackgroundDesignCommand))]
        private DeckBackgroundDesignModel selectedDeckBackgroundDesign;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AssignDeckBackgroundDesignCommand))]
        private DeckBackgroundDesignModel selectedCharacterDeckBackgroundDesign;

        [ObservableProperty]
        private ObservableCollection<DeckBackgroundDesignModel> allDeckBackgroundDesigns;

        #endregion

        #region CharacterDecks

        [ObservableProperty]
        private CharacterCardModel testCharacterCard;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCharacterDeckDesignCommand))]
        private string addedCharacterDeckDesignName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AssignCharacterDeckDesignCommand))]
        private CharacterDeckDesignModel selectedCharacterDeckDesign;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AssignCharacterDeckDesignCommand))]
        private CharacterDeckModel selectedCharacterDeck;

        [ObservableProperty]
        private ObservableCollection<CharacterDeckDesignModel> allCharacterDeckDesigns;

        [ObservableProperty]
        private ObservableCollection<CharacterDeckModel> allCharacterDecks;

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

        #region Constructor

        public DeckDesignViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            Name = Regex.Replace(nameof(DeckDesignViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Create, view and edit Card designs";
            Type = ViewModelType.DeckDesigner;

            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;

            SetUnsetDatabaseEvents(true);

            LoadData();

            SetSelectionFromNavigation();
        }

        #endregion

        #region Private methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllCharacters = new(_cardDesignerStore.Characters);
            AllSpellDecks = new(_cardDesignerStore.SpellDecks);
            AllItemDecks = new(_cardDesignerStore.ItemDecks);
            AllCharacterDecks = new(_cardDesignerStore.CharacterDecks);
            AllSpellDeckDesigns = new(_cardDesignerStore.SpellDeckDesigns);
            AllItemDeckDesigns = new(_cardDesignerStore.ItemDeckDesigns);
            AllCharacterDeckDesigns = new(_cardDesignerStore.CharacterDeckDesigns);
            AllDeckBackgroundDesigns = new(_cardDesignerStore.DeckBackgroundDesigns);
        }

        private void SetUnsetDatabaseEvents(bool set)
        {
            if (set)
            {
                _cardDesignerStore.CharacterChanged += OnCharacterChanged;
                _cardDesignerStore.CharacterDeckDesignChanged += OnCharacterDeckDesignChanged;
                _cardDesignerStore.SpellDeckDesignChanged += OnSpellDeckDesignChanged;
                _cardDesignerStore.ItemDeckDesignChanged += OnItemDeckDesignChanged;
                _cardDesignerStore.DeckBackgroundDesignChanged += OnDeckBackgroundDesignChanged;
                _navigationStore.CurrentViewModelChanged += OnNavigatingAway;
            }
            else
            {
                _cardDesignerStore.CharacterChanged -= OnCharacterChanged;
                _cardDesignerStore.CharacterDeckDesignChanged -= OnCharacterDeckDesignChanged;
                _cardDesignerStore.SpellDeckDesignChanged -= OnSpellDeckDesignChanged;
                _cardDesignerStore.ItemDeckDesignChanged -= OnItemDeckDesignChanged;
                _cardDesignerStore.DeckBackgroundDesignChanged -= OnDeckBackgroundDesignChanged;
                _navigationStore.CurrentViewModelChanged -= OnNavigatingAway;
            }
        }

        private void GetCharacterSpellDecks()
        {
            int selectedDeckID = SelectedSpellDeck?.ID ?? -1;
            int selectedCardID = TestSpellCard?.ID ?? -1;
            int selectedDesignID = SelectedSpellDeckDesign?.ID ?? -1;

            CharacterSpellDecks = new();
            if (SelectedCharacter?.SpellDeckDescriptors != null)
            {
                foreach (SpellDeckDesignLinkerModel deckDescriptor in SelectedCharacter.SpellDeckDescriptors)
                {
                    CharacterSpellDecks.Add(AllSpellDecks.First(i => i.ID == deckDescriptor.SpellDeckID));
                }
            }

            SelectedSpellDeck = CharacterSpellDecks.FirstOrDefault(cd => cd.ID == selectedDeckID) ?? CharacterSpellDecks.FirstOrDefault();
            TestSpellCard = SelectedSpellDeck?.SpellCards.FirstOrDefault(cc => cc.ID == selectedCardID) ?? SelectedSpellDeck?.SpellCards.FirstOrDefault();
            SelectedSpellDeckDesign = AllSpellDeckDesigns.FirstOrDefault(cd => cd.ID == selectedDesignID) ?? AllSpellDeckDesigns.FirstOrDefault();
        }

        private void GetCharacterItemDecks()
        {
            int selectedDeckID = SelectedItemDeck?.ID ?? -1;
            int selectedCardID = TestItemCard?.ID ?? -1;
            int selectedDesignID = SelectedItemDeckDesign?.ID ?? -1;

            CharacterItemDecks = new();
            if (SelectedCharacter?.ItemDeckDescriptors != null)
            {
                foreach (ItemDeckDesignLinkerModel deckDescriptor in SelectedCharacter.ItemDeckDescriptors)
                {
                    CharacterItemDecks.Add(AllItemDecks.First(i => i.ID == deckDescriptor.ItemDeckID));
                }
            }

            SelectedItemDeck = CharacterItemDecks.FirstOrDefault(cd => cd.ID == selectedDeckID) ?? CharacterItemDecks.FirstOrDefault();
            TestItemCard = SelectedItemDeck?.ItemCards.FirstOrDefault(cc => cc.ID == selectedCardID) ?? SelectedItemDeck?.ItemCards.FirstOrDefault();
            SelectedItemDeckDesign = AllItemDeckDesigns.FirstOrDefault(cd => cd.ID == selectedDesignID) ?? AllItemDeckDesigns.FirstOrDefault();
        }

        private void GetCharacterCharacterDecks()
        {
            int selectedDeckID = SelectedCharacterDeck?.ID ?? -1;
            int selectedCardID = TestCharacterCard?.ID ?? -1;
            int selectedDesignID = SelectedCharacterDeckDesign?.ID ?? -1;

            CharacterCharacterDecks = new();
            if (SelectedCharacter?.ItemDeckDescriptors != null)
            {
                foreach (CharacterDeckDesignLinkerModel deckDescriptor in SelectedCharacter.CharacterDeckDescriptors)
                {
                    CharacterCharacterDecks.Add(AllCharacterDecks.First(i => i.ID == deckDescriptor.CharacterDeckID));
                }
            }

            SelectedCharacterDeck = CharacterCharacterDecks.FirstOrDefault(cd => cd.ID == selectedDeckID) ?? CharacterCharacterDecks.FirstOrDefault();
            TestCharacterCard = SelectedCharacterDeck?.CharacterCards.FirstOrDefault(cc => cc.ID == selectedCardID) ?? SelectedCharacterDeck?.CharacterCards.FirstOrDefault();
            SelectedCharacterDeckDesign = AllCharacterDeckDesigns.FirstOrDefault(cd => cd.ID == selectedDesignID) ?? AllCharacterDeckDesigns.FirstOrDefault();
        }

        private void GetCharacterBackgroundDeck()
        {
            SelectedDeckBackgroundDesign = SelectedCharacter?.DeckBackgroundDesign != null
                ? AllDeckBackgroundDesigns.FirstOrDefault(dd => dd.ID == SelectedCharacter.DeckBackgroundDesign.ID)
                : AllDeckBackgroundDesigns.FirstOrDefault();
        }

        #endregion

        #region Public methods

        public static DeckDesignViewModel LoadViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            DeckDesignViewModel viewModel = new(cardDesignerStore, navigationStore);

            viewModel.LoadData();

            return viewModel;
        }

        partial void OnSelectedCharacterChanged(CharacterModel characterModel)
        {
            if (SelectedCharacter != null)
            {
            }
        }

        partial void OnSelectedTabItemChanged(int value)
        {
            switch (value)
            {
                case 0:
                    GetCharacterBackgroundDeck();
                    break;
                case 1:
                    GetCharacterCharacterDecks();
                    break;
                case 2:
                    GetCharacterSpellDecks();
                    break;
                case 3:
                    GetCharacterItemDecks();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Database update methods

        private void OnItemDeckDesignChanged(ItemDeckDesignModel itemDeckDesign, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllItemDeckDesigns.Add(itemDeckDesign);
                    SelectedItemDeckDesign = itemDeckDesign;
                    break;
                case DataChangeType.Removed:
                    break;
                case DataChangeType.Updated:
                    break;
                case DataChangeType.Deleted:
                    AllItemDeckDesigns.Remove(itemDeckDesign);
                    SelectedItemDeckDesign = AllItemDeckDesigns.FirstOrDefault();
                    break;
                default:
                    break;
            }
        }

        private void OnDeckBackgroundDesignChanged(DeckBackgroundDesignModel characterDeckDesign, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllDeckBackgroundDesigns.Add(characterDeckDesign);
                    SelectedDeckBackgroundDesign = characterDeckDesign;
                    break;
                case DataChangeType.Removed:
                    break;
                case DataChangeType.Updated:
                    break;
                case DataChangeType.Deleted:
                    AllDeckBackgroundDesigns.Remove(characterDeckDesign);
                    SelectedDeckBackgroundDesign = AllDeckBackgroundDesigns.FirstOrDefault();
                    break;
                default:
                    break;
            }
        }

        private void OnCharacterDeckDesignChanged(CharacterDeckDesignModel CharacterDeckDesign, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllCharacterDeckDesigns.Add(CharacterDeckDesign);
                    SelectedCharacterDeckDesign = CharacterDeckDesign;
                    break;
                case DataChangeType.Removed:
                    break;
                case DataChangeType.Updated:
                    break;
                case DataChangeType.Deleted:
                    AllCharacterDeckDesigns.Remove(CharacterDeckDesign);
                    SelectedCharacterDeckDesign = AllCharacterDeckDesigns.FirstOrDefault();
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
                    GetCharacterCharacterDecks();
                    break;
                case DataChangeType.Deleted:
                    AllCharacters.Remove(SelectedCharacter);
                    SelectedCharacter = AllCharacters.FirstOrDefault();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Navigation

        private void SetSelectionFromNavigation()
        {
            if (_navigationStore != null)
            {
                if (_navigationStore.UseSelection)
                {
                    switch (_navigationStore.CurrentViewModel.Type)
                    {
                        case ViewModelType.Home:
                            SelectedCharacter = _navigationStore.SelectedCharacter;
                            switch (_navigationStore.SelectedCardType)
                            {
                                case CardType.Spell:
                                    TestSpellCard = _navigationStore.SelectedSpellCard;
                                    SelectedSpellDeckDesign = _navigationStore.SelectedSpellDeckDesign;
                                    SelectedSpellDeck = _navigationStore.SelectedSpellDeck;
                                    SelectedTabItem = 2;
                                    break;
                                case CardType.Item:
                                    TestItemCard = _navigationStore.SelectedItemCard;
                                    SelectedItemDeckDesign = _navigationStore.SelectedItemDeckDesign;
                                    SelectedItemDeck = _navigationStore.SelectedItemDeck;
                                    SelectedTabItem = 3;
                                    break;
                                case CardType.Character:
                                    SelectedCharacterDeck = _navigationStore.SelectedCharacterDeck;
                                    TestCharacterCard = _navigationStore.SelectedCharacterCard;
                                    SelectedCharacterDeckDesign = _navigationStore.SelectedCharacterDeckDesign;
                                    SelectedTabItem = 1;
                                    break;
                                default:
                                    break;
                            }
                            return;
                        default:
                            break;
                    }
                }
                else
                {
                    SelectedCharacter = AllCharacters.FirstOrDefault();
                    GetCharacterBackgroundDeck();
                }
            }
        }

        private void OnNavigatingAway(ViewModelType type)
        {
            SetUnsetDatabaseEvents(false);
        }

        #endregion

        #region Commands

        #region CharacterDecks

        private bool CanAssignCharacterDeckDesign()
        {
            return SelectedCharacter != null && SelectedCharacterDeckDesign != null;
        }

        private bool CanCreateCharacterDeckDesign()
        {
            bool noName = (AddedCharacterDeckDesignName == string.Empty || AddedCharacterDeckDesignName == null);

            return !noName;
        }

        [RelayCommand(CanExecute = nameof(CanAssignCharacterDeckDesign))]
        private async void AssignCharacterDeckDesign()
        {
            await _cardDesignerStore.UpdateCardDesign(SelectedCharacterDeckDesign);
            if (SelectedCharacterDeckDesign != null)
            {
                SelectedCharacter.CharacterDeckDescriptors.FirstOrDefault(c => c.CharacterDeckID == SelectedCharacterDeck.ID).DesignID = SelectedCharacterDeckDesign.ID;
            }
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand(CanExecute = nameof(CanCreateCharacterDeckDesign))]
        private async void CreateCharacterDeckDesign()
        {
            await _cardDesignerStore.CreateCardDesign(new CharacterDeckDesignModel() { Name = AddedCharacterDeckDesignName });
        }

        [RelayCommand]
        private async void UpdateCharacterDeckDesign()
        {
            await _cardDesignerStore.UpdateCardDesign(SelectedCharacterDeckDesign);
        }

        [RelayCommand]
        private async void DeleteCharacterDeckDesign()
        {
            await _cardDesignerStore.DeleteCardDesign(SelectedCharacterDeckDesign);
        }

        #endregion

        #region ItemDecks

        private bool CanAssignItemDeckDesign()
        {
            return SelectedCharacter != null && SelectedItemDeckDesign != null && SelectedItemDeck != null;
        }

        private bool CanCreateItemDeckDesign()
        {
            bool noName = (AddedItemDeckDesignName == string.Empty || AddedItemDeckDesignName == null);

            return !noName;
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

        [RelayCommand(CanExecute = nameof(CanCreateItemDeckDesign))]
        private async void CreateItemDeckDesign()
        {
            await _cardDesignerStore.CreateCardDesign(new ItemDeckDesignModel() { Name = AddedItemDeckDesignName });
        }

        [RelayCommand]
        private async void UpdateItemDeckDesign()
        {
            await _cardDesignerStore.UpdateCardDesign(SelectedItemDeckDesign);
        }

        [RelayCommand]
        private async void DeleteItemDeckDesign()
        {
            await _cardDesignerStore.DeleteCardDesign(SelectedItemDeckDesign);
        }

        #endregion

        #region SpellDecks

        private bool CanCreateSpellDeckDesign()
        {
            bool noName = (AddedSpellDeckDesignName == string.Empty || AddedSpellDeckDesignName == null);

            return !noName;
        }

        private bool CanAssignSpellDeckDesign()
        {
            return SelectedCharacter != null && SelectedSpellDeckDesign != null && SelectedSpellDeck != null;
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

        [RelayCommand(CanExecute = nameof(CanCreateSpellDeckDesign))]
        private async void CreateSpellDeckDesign()
        {
            await _cardDesignerStore.CreateCardDesign(new SpellDeckDesignModel() { Name = AddedSpellDeckDesignName });
        }

        [RelayCommand]
        private async void UpdateSpellDeckDesign()
        {
            await _cardDesignerStore.UpdateCardDesign(SelectedSpellDeckDesign);
        }

        [RelayCommand]
        private async void DeleteSpellDeckDesign()
        {
            await _cardDesignerStore.DeleteCardDesign(SelectedSpellDeckDesign);
        }

        #endregion

        #region DeckBackgrounds

        private bool CanCreateDeckBackgroundDesign()
        {
            bool noName = (AddedDeckBackgroundDesignName == string.Empty || AddedDeckBackgroundDesignName == null);

            return !noName;
        }

        private bool CanAssignDeckBackgroundDesign()
        {
            return SelectedCharacter != null && SelectedDeckBackgroundDesign != null;
        }

        [RelayCommand(CanExecute = nameof(CanAssignDeckBackgroundDesign))]
        private async void AssignDeckBackgroundDesign()
        {
            await _cardDesignerStore.UpdateCardDesign(SelectedDeckBackgroundDesign);
            if (SelectedDeckBackgroundDesign != null)
            {
                SelectedCharacter.DeckBackgroundDesign = SelectedDeckBackgroundDesign;
            }
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand(CanExecute = nameof(CanCreateDeckBackgroundDesign))]
        private async void CreateDeckBackgroundDesign()
        {
            await _cardDesignerStore.CreateCardDesign(new DeckBackgroundDesignModel() { Name = AddedDeckBackgroundDesignName });
        }

        [RelayCommand]
        private async void UpdateDeckBackgroundDesign()
        {
            await _cardDesignerStore.UpdateCardDesign(SelectedDeckBackgroundDesign);
        }

        [RelayCommand]
        private async void DeleteDeckBackgroundDesign()
        {
            await _cardDesignerStore.DeleteCardDesign(SelectedDeckBackgroundDesign);
        }

        #endregion

        #endregion
    }
}
