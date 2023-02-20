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
    public partial class CardDecksViewModel : ViewModelBase
    {

        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;
        private readonly NavigationStore _navigationStore;

        #endregion

        #region Properties

        [ObservableProperty]
        private ICard selectedCard;

        #region Spells

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateSpellDeckCommand))]
        private string addedSpellDeckName;

        [ObservableProperty]
        private SpellDeckModel selectedSpellDeck;

        [ObservableProperty]
        private SpellCardModel selectedSpellCard;

        [ObservableProperty]
        private ObservableCollection<SpellCardModel> allSpellCards;

        [ObservableProperty]
        private ObservableCollection<SpellDeckModel> allSpellDecks;

        [ObservableProperty]
        private ObservableCollection<SpellDeckModel> characterSpellDecks;

        #endregion

        #region Items

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateItemDeckCommand))]
        private string addedItemDeckName;

        [ObservableProperty]
        private ItemDeckModel selectedItemDeck;

        [ObservableProperty]
        private ItemCardModel selectedItemCard;

        [ObservableProperty]
        private ObservableCollection<ItemCardModel> allItemCards;

        [ObservableProperty]
        private ObservableCollection<ItemDeckModel> allItemDecks;

        [ObservableProperty]
        private ObservableCollection<ItemDeckModel> characterItemDecks;

        #endregion

        #region Characters

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCharacterDeckCommand))]
        private string addedCharacterDeckName;

        [ObservableProperty]
        private CharacterDeckModel selectedCharacterDeck;

        [ObservableProperty]
        private CharacterCardModel selectedCharacterCard;

        [ObservableProperty]
        private ObservableCollection<CharacterCardModel> allCharacterCards;

        [ObservableProperty]
        private ObservableCollection<CharacterDeckModel> allCharacterDecks;

        [ObservableProperty]
        private CharacterModel selectedCharacter;

        [ObservableProperty]
        private ObservableCollection<CharacterModel> allCharacters;

        [ObservableProperty]
        private ObservableCollection<CharacterDeckModel> characterCharacterDecks;

        #endregion

        #endregion

        #region Constructor

        public CardDecksViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            Name = Regex.Replace(nameof(CardDecksViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Create, view and edit Spell Decks";
            Type = ViewModelType.DeckCreator;

            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;

            _cardDesignerStore.SpellDeckChanged += OnSpellDeckChanged;
            _cardDesignerStore.ItemDeckChanged += OnItemDeckChanged;
            _cardDesignerStore.CharacterDeckChanged += OnCharacterDeckChanged;
            _cardDesignerStore.CharacterChanged += OnCharacterChanged;

            LoadData();

            SetSelectionFromNavigation();
        }

        #endregion

        #region Private methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllSpellCards = new(_cardDesignerStore.SpellCards);
            AllItemCards = new(_cardDesignerStore.ItemCards);
            AllCharacterCards = new(_cardDesignerStore.CharacterCards);

            AllSpellDecks = new(_cardDesignerStore.SpellDecks);
            AllItemDecks = new(_cardDesignerStore.ItemDecks);
            AllCharacterDecks = new(_cardDesignerStore.CharacterDecks);
            AllCharacters = new(_cardDesignerStore.Characters);

            SelectedSpellDeck = AllSpellDecks.FirstOrDefault();
            SelectedItemDeck = AllItemDecks.FirstOrDefault();
            SelectedCharacterDeck = AllCharacterDecks.FirstOrDefault();
            SelectedCharacter = AllCharacters.FirstOrDefault();
        }

        private void SetSelectionFromNavigation()
        {
            if (_navigationStore != null)
            {
                switch (_navigationStore.CurrentViewModel.Type)
                {
                    case ViewModelType.Unknown:
                        return;
                    case ViewModelType.Home:
                        SelectedCharacter = _navigationStore.SelectedCharacter;
                        switch (_navigationStore.SelectedCardType)
                        {
                            case CardType.Spell:
                                SelectedSpellDeck= _navigationStore.SelectedSpellDeck;
                                break;
                            case CardType.Item:
                                SelectedItemDeck = _navigationStore.SelectedItemDeck;
                                break;
                            case CardType.Character:
                                SelectedCharacterDeck = _navigationStore.SelectedCharacterDeck;
                                break;
                            default:
                                break;
                        }
                        return;
                    case ViewModelType.SpellCardCreator:
                        return;
                    case ViewModelType.ItemCardCreator:
                        return;
                    case ViewModelType.DeckCreator:
                        return;
                    case ViewModelType.CharacterCreator:
                        return;
                    case ViewModelType.DeckDesigner:
                        SelectedSpellDeck = _navigationStore.SelectedSpellDeck;
                        SelectedItemDeck = _navigationStore.SelectedItemDeck;
                        return;
                    default:
                        break;
                }
            }
            SelectedSpellDeck = AllSpellDecks.FirstOrDefault();
            SelectedItemDeck = AllItemDecks.FirstOrDefault();
        }

        public static CardDecksViewModel LoadViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            CardDecksViewModel viewModel = new(cardDesignerStore, navigationStore);
            viewModel.LoadData();

            return viewModel;
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
        }

        private void GetCharacterCharacterDecks()
        {
            CharacterCharacterDecks = new();
            if (SelectedCharacter?.CharacterDeckDescriptors != null)
            {
                foreach (CharacterDeckDesignLinkerModel deckDescriptor in SelectedCharacter.CharacterDeckDescriptors)
                {
                    CharacterCharacterDecks.Add(AllCharacterDecks.First(i => i.ID == deckDescriptor.CharacterDeckID));
                }
            }
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
        }

        #endregion

        #region Database update methods

        private void OnCharacterChanged(CharacterModel characterModel, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllCharacters.Add(characterModel);
                    SelectedCharacter = characterModel;
                    break;
                case DataChangeType.Updated:
                    SelectedCharacter = characterModel;
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

        private void OnSpellDeckChanged(SpellDeckModel spellDeck, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllSpellDecks.Add(spellDeck);
                    SelectedSpellDeck = spellDeck;
                    break;
                case DataChangeType.Updated:
                    SelectedSpellDeck = spellDeck;
                    break;
                case DataChangeType.Deleted:
                    AllSpellDecks.Remove(SelectedSpellDeck);
                    SelectedSpellDeck = AllSpellDecks.FirstOrDefault();
                    break;
                default:
                    break;
            }
        }

        private void OnItemDeckChanged(ItemDeckModel itemDeck, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllItemDecks.Add(itemDeck);
                    SelectedItemDeck = itemDeck;
                    break;
                case DataChangeType.Updated:
                    SelectedItemDeck = itemDeck;
                    break;
                case DataChangeType.Deleted:
                    AllItemDecks.Remove(SelectedItemDeck);
                    SelectedItemDeck = AllItemDecks.FirstOrDefault();
                    break;
                default:
                    break;
            }
        }

        private void OnCharacterDeckChanged(CharacterDeckModel characterDeck, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllCharacterDecks.Add(characterDeck);
                    SelectedCharacterDeck = characterDeck;
                    break;
                case DataChangeType.Updated:
                    SelectedCharacterDeck = characterDeck;
                    break;
                case DataChangeType.Deleted:
                    AllCharacterDecks.Remove(SelectedCharacterDeck);
                    SelectedCharacterDeck = AllCharacterDecks.FirstOrDefault();
                    break;
                default:
                    break;
            }
        }

        partial void OnSelectedCharacterChanged(CharacterModel characterModel)
        {
            GetCharacterSpellDecks();
            GetCharacterItemDecks();
            GetCharacterCharacterDecks();
        }

        #endregion

        #region Commands

        #region SpellDecks

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
        private async void AddSpellCardToDeck(SpellCardModel spellCard)
        {
            SelectedSpellDeck.SpellCards.Add(spellCard);
            await _cardDesignerStore.UpdateSpellDeck(SelectedSpellDeck);
        }

        [RelayCommand]
        private async void RemoveSpellCardFromDeck(SpellCardModel spellCard)
        {
            SelectedSpellDeck.SpellCards.Remove(spellCard);
            await _cardDesignerStore.UpdateSpellDeck(SelectedSpellDeck);
        }

        [RelayCommand]
        private async void DeleteSpellDeck()
        {
            await _cardDesignerStore.DeleteSpellDeck(SelectedSpellDeck);
        }

        [RelayCommand]
        private async void AddSpellDeckToCharacter(SpellDeckModel spellDeck)
        {
            if (!SelectedCharacter.SpellDeckDescriptors.Any(d => d.SpellDeckID == spellDeck.ID))
            {
                SelectedCharacter.SpellDeckDescriptors.Add(new()
                {
                    SpellDeckID = spellDeck.ID,
                });
                await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
            }
        }

        [RelayCommand]
        private async void RemoveSpellDeckFromCharacter(SpellDeckModel spellDeck)
        {
            SpellDeckDesignLinkerModel toRemove = SelectedCharacter.SpellDeckDescriptors.FirstOrDefault(sd => sd.SpellDeckID == spellDeck.ID);
            SelectedCharacter.SpellDeckDescriptors.Remove(toRemove);
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        #endregion

        #region ItemDecks

        [RelayCommand(CanExecute = nameof(CanCreateItemDeck))]
        private async void CreateItemDeck()
        {
            await _cardDesignerStore.CreateItemDeck(new ItemDeckModel() { Name = AddedItemDeckName });
        }

        private bool CanCreateItemDeck()
        {
            bool noName = (AddedItemDeckName == string.Empty || AddedItemDeckName == null);
            bool itemDeckExists = AllItemDecks == null ? false : AllItemDecks.Where(c => c.Name == AddedItemDeckName).Any();

            return (!noName && !itemDeckExists);
        }

        [RelayCommand]
        private async void AddItemCardToDeck(ItemCardModel itemCard)
        {
            SelectedItemDeck.ItemCards.Add(itemCard);
            await _cardDesignerStore.UpdateItemDeck(SelectedItemDeck);
        }

        [RelayCommand]
        private async void RemoveItemCardFromDeck(ItemCardModel itemCard)
        {
            SelectedItemDeck.ItemCards.Remove(itemCard);
            await _cardDesignerStore.UpdateItemDeck(SelectedItemDeck);
        }

        [RelayCommand]
        private async void DeleteItemDeck()
        {
            await _cardDesignerStore.DeleteItemDeck(SelectedItemDeck);
        }
        [RelayCommand]
        private async void RemoveItemDeckFromCharacter(ItemDeckModel itemDeck)
        {
            ItemDeckDesignLinkerModel toRemove = SelectedCharacter.ItemDeckDescriptors.FirstOrDefault(sd => sd.ItemDeckID == itemDeck.ID);
            SelectedCharacter.ItemDeckDescriptors.Remove(toRemove);
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand]
        private async void AddItemDeckToCharacter(ItemDeckModel itemDeck)
        {
            if (!SelectedCharacter.ItemDeckDescriptors.Any(d => d.ItemDeckID == itemDeck.ID))
            {
                SelectedCharacter.ItemDeckDescriptors.Add(new()
                {
                    ItemDeckID = itemDeck.ID,
                });
                await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
            }
        }

        #endregion

        #region CharacterDecks

        [RelayCommand(CanExecute = nameof(CanCreateCharacterDeck))]
        private async void CreateCharacterDeck()
        {
            await _cardDesignerStore.CreateCharacterDeck(new CharacterDeckModel() { Name = AddedCharacterDeckName });
        }

        private bool CanCreateCharacterDeck()
        {
            bool noName = (AddedCharacterDeckName == string.Empty || AddedCharacterDeckName == null);
            bool characterDeckExists = AllCharacterDecks == null ? false : AllCharacterDecks.Where(c => c.Name == AddedCharacterDeckName).Any();

            return (!noName && !characterDeckExists);
        }

        [RelayCommand]
        private async void AddCharacterCardToDeck(CharacterCardModel characterCard)
        {
            SelectedCharacterDeck.CharacterCards.Add(characterCard);
            await _cardDesignerStore.UpdateCharacterDeck(SelectedCharacterDeck);
        }

        [RelayCommand]
        private async void RemoveCharacterCardFromDeck(CharacterCardModel characterCard)
        {
            SelectedCharacterDeck.CharacterCards.Remove(characterCard);
            await _cardDesignerStore.UpdateCharacterDeck(SelectedCharacterDeck);
        }

        [RelayCommand]
        private async void DeleteCharacterDeck()
        {
            await _cardDesignerStore.DeleteCharacterDeck(SelectedCharacterDeck);
        }

        [RelayCommand]
        private async void AddCharacterDeckToCharacter(CharacterDeckModel CharacterDeck)
        {
            if (!SelectedCharacter.CharacterDeckDescriptors.Any(d => d.CharacterDeckID == CharacterDeck.ID))
            {
                SelectedCharacter.CharacterDeckDescriptors.Add(new()
                {
                    CharacterDeckID = CharacterDeck.ID,
                });
                await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
            }
        }

        [RelayCommand]
        private async void RemoveCharacterDeckFromCharacter(CharacterDeckModel CharacterDeck)
        {
            CharacterDeckDesignLinkerModel toRemove = SelectedCharacter.CharacterDeckDescriptors.FirstOrDefault(sd => sd.CharacterDeckID == CharacterDeck.ID);
            SelectedCharacter.CharacterDeckDescriptors.Remove(toRemove);
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        #endregion

        #endregion
    }
}
