﻿using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;

namespace CardDesigner.UI.ViewModels
{
    public partial class CardDecksViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;
        private readonly NavigationStore _navigationStore;
        private readonly SettingsStore _settingsStore;

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

        #region Minions

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateMinionDeckCommand))]
        private string addedMinionDeckName;

        [ObservableProperty]
        private MinionDeckModel selectedMinionDeck;

        [ObservableProperty]
        private MinionCardModel selectedMinionCard;

        [ObservableProperty]
        private ObservableCollection<MinionCardModel> allMinionCards;

        [ObservableProperty]
        private ObservableCollection<MinionDeckModel> allMinionDecks;

        [ObservableProperty]
        private ObservableCollection<MinionDeckModel> characterMinionDecks;

        #endregion

        #endregion

        #region Constructor

        public CardDecksViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore, SettingsStore settingsStore)
        {
            Name = ModuleNameRegex().Replace(nameof(CardDecksViewModel).Replace("ViewModel", ""), " $1");
            Description = "Create, view and edit Spell Decks";
            Type = ViewModelType.DeckCreator;

            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;
            _settingsStore = settingsStore;

            SetUnsetDatabaseEvents(true);

            LoadData();

            //SetSelectionFromNavigation();
        }

        #endregion

        #region Private methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllSpellCards = new(_cardDesignerStore.SpellCards);
            AllMinionCards = new(_cardDesignerStore.MinionCards);
            AllItemCards = new(_cardDesignerStore.ItemCards);
            AllCharacterCards = new(_cardDesignerStore.CharacterCards);

            AllSpellDecks = new(_cardDesignerStore.SpellDecks);
            AllMinionDecks = new(_cardDesignerStore.MinionDecks);
            AllItemDecks = new(_cardDesignerStore.ItemDecks);
            AllCharacterDecks = new(_cardDesignerStore.CharacterDecks);
            AllCharacters = new(_cardDesignerStore.Characters);

            SelectedSpellDeck = AllSpellDecks.FirstOrDefault();
            SelectedMinionDeck = AllMinionDecks.FirstOrDefault();
            SelectedItemDeck = AllItemDecks.FirstOrDefault();
            SelectedCharacterDeck = AllCharacterDecks.FirstOrDefault();
            SelectedCharacter = AllCharacters.FirstOrDefault();
        }

        private void GetCharacterMinionDecks()
        {
            CharacterMinionDecks = new();
            if (SelectedCharacter?.MinionDeckDescriptors != null)
            {
                foreach (MinionDeckDesignLinkerModel deckDescriptor in SelectedCharacter.MinionDeckDescriptors)
                {
                    CharacterMinionDecks.Add(AllMinionDecks.First(i => i.ID == deckDescriptor.MinionDeckID));
                }
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

        #region Public methods

        public static CardDecksViewModel LoadViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore, SettingsStore settingsStore)
        {
            CardDecksViewModel viewModel = new(cardDesignerStore, navigationStore, settingsStore);
            viewModel.LoadData();

            return viewModel;
        }

        #endregion

        #region Database update methods

        private void SetUnsetDatabaseEvents(bool set)
        {
            if (set)
            {
                _cardDesignerStore.SpellDeckChanged += OnSpellDeckChanged;
                _cardDesignerStore.MinionDeckChanged += OnMinionDeckChanged;
                _cardDesignerStore.ItemDeckChanged += OnItemDeckChanged;
                _cardDesignerStore.CharacterDeckChanged += OnCharacterDeckChanged;
                _cardDesignerStore.CharacterChanged += OnCharacterChanged;
                _navigationStore.CurrentViewModelChanged += OnNavigatingAway;
            }
            else
            {
                _cardDesignerStore.SpellDeckChanged -= OnSpellDeckChanged;
                _cardDesignerStore.MinionDeckChanged -= OnMinionDeckChanged;
                _cardDesignerStore.ItemDeckChanged -= OnItemDeckChanged;
                _cardDesignerStore.CharacterDeckChanged -= OnCharacterDeckChanged;
                _cardDesignerStore.CharacterChanged -= OnCharacterChanged;
                _navigationStore.CurrentViewModelChanged -= OnNavigatingAway;
            }
        }

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
                    GetCharacterMinionDecks();
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
                    AllSpellDecks = new(_cardDesignerStore.SpellDecks);
                    SelectedSpellDeck = AllSpellDecks.FirstOrDefault(d => d.ID == spellDeck.ID);
                    break;
                case DataChangeType.Deleted:
                    AllSpellDecks.Remove(SelectedSpellDeck);
                    SelectedSpellDeck = AllSpellDecks.FirstOrDefault();
                    break;
                default:
                    break;
            }
        }

        private void OnMinionDeckChanged(MinionDeckModel minionDeck, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllMinionDecks.Add(minionDeck);
                    SelectedMinionDeck = minionDeck;
                    break;
                case DataChangeType.Updated:
                    AllMinionDecks = new(_cardDesignerStore.MinionDecks);
                    SelectedMinionDeck = AllMinionDecks.FirstOrDefault(d => d.ID == minionDeck.ID);
                    break;
                case DataChangeType.Deleted:
                    AllMinionDecks.Remove(SelectedMinionDeck);
                    SelectedMinionDeck = AllMinionDecks.FirstOrDefault();
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
                    AllItemDecks = new(_cardDesignerStore.ItemDecks);
                    SelectedItemDeck = AllItemDecks.FirstOrDefault(d => d.ID == itemDeck.ID);
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
                    AllCharacterDecks = new(_cardDesignerStore.CharacterDecks);
                    SelectedCharacterDeck = AllCharacterDecks.FirstOrDefault(d => d.ID == characterDeck.ID);
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
            GetCharacterMinionDecks();
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
                                    SelectedSpellDeck = _navigationStore.SelectedSpellDeck;
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
                        default:
                            break;
                    }
                }
                else
                {
                    SelectedCharacter = AllCharacters.FirstOrDefault();
                    SelectedSpellDeck = AllSpellDecks.FirstOrDefault();
                    SelectedItemDeck = AllItemDecks.FirstOrDefault();
                    SelectedCharacterDeck = AllCharacterDecks.FirstOrDefault();
                }
            }
        }

        private void OnNavigatingAway(ViewModelType type)
        {
            SetUnsetDatabaseEvents(false);
        }

        #endregion

        #region Commands

        #region SpellDecks

        [RelayCommand(CanExecute = nameof(CanCreateSpellDeck))]
        private async void CreateSpellDeck()
        {
            await _cardDesignerStore.CreateSpellDeck(new SpellDeckModel() { Name = AddedSpellDeckName, Title = "Enter name" });
        }

        private bool CanCreateSpellDeck()
        {
            bool noName = (AddedSpellDeckName == string.Empty || AddedSpellDeckName == null);
            bool spellDeckExists = AllSpellDecks != null && AllSpellDecks.Where(c => c.Name == AddedSpellDeckName).Any();

            return (!noName && !spellDeckExists);
        }

        [RelayCommand]
        private async void AddSpellCardToDeck(SpellCardModel spellCard)
        {
            SelectedSpellDeck.Cards.Add(spellCard);
            await _cardDesignerStore.UpdateSpellDeck(SelectedSpellDeck);
        }

        [RelayCommand]
        private async void RemoveSpellCardFromDeck(SpellCardModel spellCard)
        {
            SelectedSpellDeck.Cards.Remove(spellCard);
            await _cardDesignerStore.UpdateSpellDeck(SelectedSpellDeck);
        }

        [RelayCommand]
        private async void DeleteSpellDeck()
        {
            SpellDeckDesignLinkerModel toRemove = SelectedCharacter.SpellDeckDescriptors.FirstOrDefault(dd => dd.SpellDeckID == SelectedSpellDeck.ID);
            if (toRemove != null)
            {
                SelectedCharacter.SpellDeckDescriptors.Remove(toRemove);
            }

            await _cardDesignerStore.DeleteSpellDeck(SelectedSpellDeck);

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
        private async void UpdateSpellDeck()
        {
            await _cardDesignerStore.UpdateSpellDeck(SelectedSpellDeck);
        }

        #endregion

        #region ItemDecks

        [RelayCommand(CanExecute = nameof(CanCreateItemDeck))]
        private async void CreateItemDeck()
        {
            await _cardDesignerStore.CreateItemDeck(new ItemDeckModel() { Name = AddedItemDeckName, Title = AddedItemDeckName });
        }

        private bool CanCreateItemDeck()
        {
            bool noName = (AddedItemDeckName == string.Empty || AddedItemDeckName == null);
            bool itemDeckExists = AllItemDecks != null && AllItemDecks.Where(c => c.Name == AddedItemDeckName).Any();

            return (!noName && !itemDeckExists);
        }

        [RelayCommand]
        private async void AddItemCardToDeck(ItemCardModel itemCard)
        {
            SelectedItemDeck.Cards.Add(itemCard);
            await _cardDesignerStore.UpdateItemDeck(SelectedItemDeck);
        }

        [RelayCommand]
        private async void RemoveItemCardFromDeck(ItemCardModel itemCard)
        {
            SelectedItemDeck.Cards.Remove(itemCard);
            await _cardDesignerStore.UpdateItemDeck(SelectedItemDeck);
        }

        [RelayCommand]
        private async void DeleteItemDeck()
        {
            ItemDeckDesignLinkerModel toRemove = SelectedCharacter.ItemDeckDescriptors.FirstOrDefault(dd => dd.ItemDeckID == SelectedItemDeck.ID);
            if (toRemove != null)
            {
                SelectedCharacter.ItemDeckDescriptors.Remove(toRemove);
            }

            await _cardDesignerStore.DeleteItemDeck(SelectedItemDeck);

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

        [RelayCommand]
        private async void UpdateItemDeck()
        {
            await _cardDesignerStore.UpdateItemDeck(SelectedItemDeck);
        }

        #endregion

        #region CharacterDecks

        [RelayCommand(CanExecute = nameof(CanCreateCharacterDeck))]
        private async void CreateCharacterDeck()
        {
            await _cardDesignerStore.CreateCharacterDeck(new CharacterDeckModel() { Name = AddedCharacterDeckName, Title = AddedCharacterDeckName });
        }

        private bool CanCreateCharacterDeck()
        {
            bool noName = (AddedCharacterDeckName == string.Empty || AddedCharacterDeckName == null);
            bool characterDeckExists = AllCharacterDecks != null && AllCharacterDecks.Where(c => c.Name == AddedCharacterDeckName).Any();

            return (!noName && !characterDeckExists);
        }

        [RelayCommand]
        private async void AddCharacterCardToDeck(CharacterCardModel characterCard)
        {
            SelectedCharacterDeck.Cards.Add(characterCard);
            await _cardDesignerStore.UpdateCharacterDeck(SelectedCharacterDeck);
        }

        [RelayCommand]
        private async void RemoveCharacterCardFromDeck(CharacterCardModel characterCard)
        {
            SelectedCharacterDeck.Cards.Remove(characterCard);
            await _cardDesignerStore.UpdateCharacterDeck(SelectedCharacterDeck);
        }

        [RelayCommand]
        private async void DeleteCharacterDeck()
        {
            CharacterDeckDesignLinkerModel toRemove = SelectedCharacter.CharacterDeckDescriptors.FirstOrDefault(dd => dd.CharacterDeckID == SelectedCharacterDeck.ID);
            if (toRemove != null)
            {
                SelectedCharacter.CharacterDeckDescriptors.Remove(toRemove);
            }

            await _cardDesignerStore.DeleteCharacterDeck(SelectedCharacterDeck);

            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand]
        private async void RemoveCharacterDeckFromCharacter(CharacterDeckModel CharacterDeck)
        {
            CharacterDeckDesignLinkerModel toRemove = SelectedCharacter.CharacterDeckDescriptors.FirstOrDefault(sd => sd.CharacterDeckID == CharacterDeck.ID);
            SelectedCharacter.CharacterDeckDescriptors.Remove(toRemove);
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
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
        private async void UpdateCharacterDeck()
        {
            await _cardDesignerStore.UpdateCharacterDeck(SelectedCharacterDeck);
        }

        #endregion

        #region MinionDecks

        [RelayCommand(CanExecute = nameof(CanCreateMinionDeck))]
        private async void CreateMinionDeck()
        {
            await _cardDesignerStore.CreateMinionDeck(new MinionDeckModel() { Name = AddedMinionDeckName, Title = AddedMinionDeckName });
        }

        private bool CanCreateMinionDeck()
        {
            bool noName = (AddedMinionDeckName == string.Empty || AddedMinionDeckName == null);
            bool MinionDeckExists = AllMinionDecks != null && AllMinionDecks.Where(c => c.Name == AddedMinionDeckName).Any();

            return (!noName && !MinionDeckExists);
        }

        [RelayCommand]
        private async void AddMinionCardToDeck(MinionCardModel MinionCard)
        {
            SelectedMinionDeck.Cards.Add(MinionCard);
            await _cardDesignerStore.UpdateMinionDeck(SelectedMinionDeck);
        }

        [RelayCommand]
        private async void RemoveMinionCardFromDeck(MinionCardModel MinionCard)
        {
            SelectedMinionDeck.Cards.Remove(MinionCard);
            await _cardDesignerStore.UpdateMinionDeck(SelectedMinionDeck);
        }

        [RelayCommand]
        private async void DeleteMinionDeck()
        {
            MinionDeckDesignLinkerModel toRemove = SelectedCharacter.MinionDeckDescriptors.FirstOrDefault(dd => dd.MinionDeckID == SelectedMinionDeck.ID);
            if (toRemove != null)
            {
                SelectedCharacter.MinionDeckDescriptors.Remove(toRemove);
            }

            await _cardDesignerStore.DeleteMinionDeck(SelectedMinionDeck);

            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand]
        private async void RemoveMinionDeckFromCharacter(MinionDeckModel MinionDeck)
        {
            MinionDeckDesignLinkerModel toRemove = SelectedCharacter.MinionDeckDescriptors.FirstOrDefault(sd => sd.MinionDeckID == MinionDeck.ID);
            SelectedCharacter.MinionDeckDescriptors.Remove(toRemove);
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand]
        private async void AddMinionDeckToCharacter(MinionDeckModel MinionDeck)
        {
            if (!SelectedCharacter.MinionDeckDescriptors.Any(d => d.MinionDeckID == MinionDeck.ID))
            {
                SelectedCharacter.MinionDeckDescriptors.Add(new()
                {
                    MinionDeckID = MinionDeck.ID,
                });
                await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
            }
        }

        [RelayCommand]
        private async void UpdateMinionDeck()
        {
            await _cardDesignerStore.UpdateMinionDeck(SelectedMinionDeck);
        }

        #endregion

        #endregion
    }
}
