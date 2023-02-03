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
        private SpellCardModel testCharacterCard;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCharacterCommand))]
        private string addedCharacterName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCharacterDeckDesignCommand))]
        private string addedCharacterDeckDesignName;

        [ObservableProperty]
        private CharacterDeckDesignModel selectedBackgroundDesign;

        [ObservableProperty]
        private CharacterDeckDesignModel selectedCharacterBackgroundDesign;

        [ObservableProperty]
        private CharacterModel selectedCharacter;

        [ObservableProperty]
        private ObservableCollection<CharacterDeckDesignModel> allCharacterDeckDesigns;

        [ObservableProperty]
        private ObservableCollection<CharacterModel> allCharacters;

        [ObservableProperty]
        private ObservableCollection<SpellDeckModel> characterSpellDecks;

        [ObservableProperty]
        private ObservableCollection<ItemDeckModel> characterItemDecks;

        #endregion

        #region ItemDecks

        [ObservableProperty]
        private ItemCardModel testItemCard;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateItemDeckDesignCommand))]
        private string addedItemDeckDesignName;

        [ObservableProperty]
        private ItemDeckDesignModel selectedItemDeckDesign;

        [ObservableProperty]
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
        private SpellDeckDesignModel selectedSpellDeckDesign;

        [ObservableProperty]
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
            selectedCharacterBackgroundDesign = AllCharacterDeckDesigns.FirstOrDefault();
            SelectedCharacter = AllCharacters.FirstOrDefault();
            SelectedItemDeck = AllItemDecks.FirstOrDefault();

            GetCharacterSpellDecks();
            GetCharacterItemDecks();
            GetCharacterBackgroundDeck();
            UpdateSpellDeckDesign();
            UpdateItemDeckDesign();
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

        private void OnCharacterDeckDesignChanged(CharacterDeckDesignModel characterDeckDesign, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllCharacterDeckDesigns.Add(characterDeckDesign);
                    SelectedBackgroundDesign = characterDeckDesign;
                    break;
                case DataChangeType.Removed:
                    break;
                case DataChangeType.Updated:
                    break;
                case DataChangeType.Deleted:
                    AllCharacterDeckDesigns.Remove(characterDeckDesign);
                    SelectedBackgroundDesign = AllCharacterDeckDesigns.FirstOrDefault();
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
            AllCharacterDeckDesigns = new(_cardDesignerStore.CharacterDeckDesigns);
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


        [RelayCommand(CanExecute = nameof(CanCreateCharacterDeckDesign))]
        private async void CreateCharacterDeckDesign()
        {
            await _cardDesignerStore.CreateCardDesign(new CharacterDeckDesignModel() { Name = AddedCharacterDeckDesignName });
        }

        private bool CanCreateCharacterDeckDesign()
        {
            bool noName = (AddedCharacterDeckDesignName == string.Empty || AddedCharacterDeckDesignName == null);

            return !noName;
        }


        [RelayCommand(CanExecute = nameof(CanCreateCharacter))]
        private async void CreateCharacter()
        {
            await _cardDesignerStore.CreateCharacter(
                new CharacterModel() { Name = AddedCharacterName, 
                    Classes = new() {
                        new CharacterClassModel() { Class = CharacterClassType.Barbarian, Level=3 },
                        new CharacterClassModel() { Class = CharacterClassType.Druid, Level=5 },
                    }});
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

        [RelayCommand]
        private async void DeleteSpellDeckDesign()
        {
            //await _cardDesignerStore.DeleteCardDesign(SelectedCardDesign);
        }
        [RelayCommand]
        private async void DeleteItemDeckDesign()
        {
            //await _cardDesignerStore.DeleteCardDesign(SelectedCardDesign);
        }
        [RelayCommand]
        private async void DeleteCharacterDeckDesign()
        {
            //await _cardDesignerStore.DeleteCardDesign(SelectedCardDesign);
        }

        [RelayCommand]
        private async void AddSpellDeckToCharacter(SpellDeckModel spellDeck)
        {
            if (SelectedCharacter.SpellDeckDescriptors.Any())
            {
                // If character has any descriptors
                if (SelectedCharacter.SpellDeckDescriptors.Any(d => d.SpellDeckID == spellDeck.ID))
                {
                    SelectedCharacter.SpellDeckDescriptors
                        .First(d => d.SpellDeckID == spellDeck.ID)
                        .DesignID = SelectedSpellDeckDesign == null ? 0 : SelectedSpellDeckDesign.ID;
                }
                else
                {
                    SelectedCharacter.SpellDeckDescriptors.Add(new()
                    {
                        SpellDeckID = spellDeck.ID,
                        DesignID = SelectedSpellDeckDesign == null ? 0 : SelectedSpellDeckDesign.ID
                    });
                }
            }
            else
            {
                // Otherwise make a new list
                SelectedCharacter.SpellDeckDescriptors = new()
                {
                    new()
                    {
                        SpellDeckID = spellDeck.ID,
                        DesignID = SelectedSpellDeckDesign == null ? 0 : SelectedSpellDeckDesign.ID
                    }
                };
            }

            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand]
        private async void AddItemDeckToCharacter(ItemDeckModel itemDeck)
        {
            if (SelectedCharacter.ItemDeckDescriptors.Any())
            {
                // If character has any descriptors
                if (SelectedCharacter.ItemDeckDescriptors.Any(d => d.ItemDeckID == itemDeck.ID))
                {
                    SelectedCharacter.ItemDeckDescriptors
                        .First(d => d.ItemDeckID == itemDeck.ID)
                        .DesignID = SelectedItemDeckDesign == null ? 0 : SelectedItemDeckDesign.ID;
                }
                else
                {
                    SelectedCharacter.ItemDeckDescriptors.Add(new()
                    {
                        ItemDeckID = itemDeck.ID,
                        DesignID = SelectedItemDeckDesign == null ? 0 : SelectedItemDeckDesign.ID
                    });
                }
            }
            else
            {
                // Otherwise make a new list
                SelectedCharacter.ItemDeckDescriptors = new()
                {
                    new()
                    {
                        ItemDeckID = itemDeck.ID,
                        DesignID = SelectedItemDeckDesign == null ? 0 : SelectedItemDeckDesign.ID
                    }
                };
            }

            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand]
        private async void RemoveSpellDeckFromCharacter(SpellDeckModel spellDeck)
        {
            SpellDeckDesignLinkerModel toRemove = SelectedCharacter.SpellDeckDescriptors.FirstOrDefault(sd => sd.SpellDeckID == spellDeck.ID);
            SelectedCharacter.SpellDeckDescriptors.Remove(toRemove);
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand]
        private async void RemoveItemDeckFromCharacter(ItemDeckModel itemDeck)
        {
            ItemDeckDesignLinkerModel toRemove = SelectedCharacter.ItemDeckDescriptors.FirstOrDefault(sd => sd.ItemDeckID == itemDeck.ID);
            SelectedCharacter.ItemDeckDescriptors.Remove(toRemove);
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
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
        private async void UpdateCharactereckDesign()
        {
            await _cardDesignerStore.UpdateCardDesign(SelectedBackgroundDesign);
        }

        partial void OnSelectedCharacterChanged(CharacterModel selectedCharacter)
        {
            GetCharacterSpellDecks();
            GetCharacterItemDecks();
            GetCharacterBackgroundDeck();
        }

        private void GetCharacterBackgroundDeck()
        {
            SelectedCharacterBackgroundDesign = SelectedCharacter?.DeckBackgroundDesign != null
                ? AllCharacterDeckDesigns.FirstOrDefault(dd => dd.ID == SelectedCharacter.DeckBackgroundDesign.ID)
                : AllCharacterDeckDesigns.FirstOrDefault();
            SelectedBackgroundDesign = SelectedCharacterBackgroundDesign;
        }

        #endregion
    }
}
