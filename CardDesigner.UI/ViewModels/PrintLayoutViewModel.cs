using CardDesigner.Domain.Enums;
using CardDesigner.Domain.HelperModels;
using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CardDesigner.UI.ViewModels
{
    public partial class PrintLayoutViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;
        private readonly NavigationStore _navigationStore;
        private readonly SettingsStore _settingsStore;

        private List<ItemDeckDesignModel> AllItemDeckDesigns;
        private List<SpellDeckDesignModel> AllSpellDeckDesigns;
        private List<MinionDeckDesignModel> AllMinionDeckDesigns;
        private List<CharacterDeckDesignModel> AllCharacterDeckDesigns;

        #endregion

        #region Properties

        [ObservableProperty]
        private ObservableCollection<BackgroundCardModel> backgroundPage;

        [ObservableProperty]
        private ObservableCollection<CardPageModel> cardPages;

        [ObservableProperty]
        private ObservableCollection<TreeItemModel> treeCharacters;

        [ObservableProperty]
        private double cardScale = 100;

        [ObservableProperty]
        private double pageOffsetX = 0;

        [ObservableProperty]
        private bool printBackside = true;

        [ObservableProperty]
        private int selectedPageIndex = -1;

        [ObservableProperty]
        private ICardDesign selectedCardDesign;

        [ObservableProperty]
        private IDeck selectedDeck;

        [ObservableProperty]
        private DeckBackgroundDesignModel selectedCharacterBacgroundDesign;

        [ObservableProperty]
        private ItemDeckModel selectedItemDeck;

        [ObservableProperty]
        private SpellDeckModel selectedSpellDeck;

        [ObservableProperty]
        private MinionDeckModel selectedMinionDeck;

        [ObservableProperty]
        private CharacterDeckModel selectedCharacterDeck;

        [ObservableProperty]
        private CharacterModel selectedCharacter;

        [ObservableProperty]
        private ObservableCollection<SpellDeckModel> allSpellDecks;

        [ObservableProperty]
        private ObservableCollection<MinionDeckModel> allMinionDecks;

        [ObservableProperty]
        private ObservableCollection<ItemDeckModel> allItemDecks;

        [ObservableProperty]
        private ObservableCollection<CharacterDeckModel> allCharacterDecks;

        [ObservableProperty]
        private ObservableCollection<CharacterModel> allCharacters;

        #endregion

        #region Constructor

        public PrintLayoutViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore, SettingsStore settingsStore)
        {
            Name = ModuleNameRegex().Replace(nameof(PrintLayoutViewModel).Replace("ViewModel", ""), " $1");
            Description = "Print layout";
            Type = ViewModelType.PrintLayout;

            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;
            _settingsStore = settingsStore;

            LoadData();

            CardScale = SettingsStore.PrintCardScale;
            PageOffsetX = SettingsStore.PrintPageOffsetX;
            PrintBackside = SettingsStore.PrintBackside;

            GenerateCharacterTree();
        }

        #endregion

        #region Private methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllCharacters = _cardDesignerStore.Characters == null ? new() : new(_cardDesignerStore.Characters);
            AllSpellDecks = _cardDesignerStore.SpellDecks == null ? new() : new(_cardDesignerStore.SpellDecks);
            AllMinionDecks = _cardDesignerStore.MinionDecks == null ? new() : new(_cardDesignerStore.MinionDecks);
            AllItemDecks = _cardDesignerStore.ItemDecks == null ? new() : new(_cardDesignerStore.ItemDecks);
            AllCharacterDecks = _cardDesignerStore.CharacterDecks == null ? new() : new(_cardDesignerStore.CharacterDecks);

            SelectedItemDeck = AllItemDecks.FirstOrDefault();
            SelectedSpellDeck = AllSpellDecks.FirstOrDefault();
            SelectedMinionDeck = AllMinionDecks.FirstOrDefault();
            SelectedCharacterDeck = AllCharacterDecks.FirstOrDefault();

            AllItemDeckDesigns = _cardDesignerStore.ItemDeckDesigns.ToList();
            AllSpellDeckDesigns = _cardDesignerStore.SpellDeckDesigns.ToList();
            AllMinionDeckDesigns = _cardDesignerStore.MinionDeckDesigns.ToList();
            AllCharacterDeckDesigns = _cardDesignerStore.CharacterDeckDesigns.ToList();

            SetSelectionFromNavigation();
        }

        private void SetUnsetDatabaseEvents(bool set)
        {
            if (set)
            {
                _navigationStore.CurrentViewModelChanged += OnNavigatingAway;
            }
            else
            {
                _navigationStore.CurrentViewModelChanged -= OnNavigatingAway;
            }
        }

        /// <summary>
        /// Create tree view listings
        /// </summary>
        private void GenerateCharacterTree()
        {
            TreeCharacters = new();
            foreach (CharacterModel character in AllCharacters)
            {
                TreeItemModel addedCharacter = new()
                {
                    Name = character.Name,
                    Title = character.Title,
                    ID = character.ID,
                    IsExpanded = true,
                    IsSelected = true
                };

                // Create Item deck tree structure
                foreach (ItemDeckDesignLinkerModel itemDeckDescriptor in character.ItemDeckDescriptors)
                {
                    ItemDeckModel itemDeck = AllItemDecks.FirstOrDefault(id => id.ID == itemDeckDescriptor.ItemDeckID);

                    TreeItemModel addedItemDeck = new()
                    {
                        Name = itemDeck?.Name,
                        Title = itemDeck?.Title,
                        ID = itemDeck.ID,
                        Item = itemDeck,
                        ParentID = character.ID
                    };
                    addedCharacter.Items.Add(addedItemDeck);
                }

                // Create Spell deck tree structure
                foreach (SpellDeckDesignLinkerModel spellDeckDescriptor in character.SpellDeckDescriptors)
                {
                    SpellDeckModel spellDeck = AllSpellDecks.FirstOrDefault(id => id.ID == spellDeckDescriptor.SpellDeckID);

                    TreeItemModel addedSpellDeck = new()
                    {
                        Name = spellDeck?.Name,
                        Title = spellDeck?.Title,
                        ID = spellDeck.ID,
                        Item = spellDeck,
                        ParentID = character.ID
                    };
                    addedCharacter.Items.Add(addedSpellDeck);
                }

                // Create Minion deck tree structure
                foreach (MinionDeckDesignLinkerModel MinionDeckDescriptor in character.MinionDeckDescriptors)
                {
                    MinionDeckModel MinionDeck = AllMinionDecks.FirstOrDefault(id => id.ID == MinionDeckDescriptor.MinionDeckID);

                    TreeItemModel addedMinionDeck = new()
                    {
                        Name = MinionDeck?.Name,
                        Title = MinionDeck?.Title,
                        ID = MinionDeck.ID,
                        Item = MinionDeck,
                        ParentID = character.ID
                    };
                    addedCharacter.Items.Add(addedMinionDeck);
                }

                // Create Character deck tree structure
                foreach (CharacterDeckDesignLinkerModel characterDeckDescriptor in character.CharacterDeckDescriptors)
                {
                    CharacterDeckModel characterDeck = AllCharacterDecks.FirstOrDefault(id => id.ID == characterDeckDescriptor.CharacterDeckID);

                    TreeItemModel addedCharacterDeck = new()
                    {
                        Name = characterDeck?.Name,
                        Title = characterDeck?.Title,
                        ID = characterDeck.ID,
                        Item = characterDeck,
                        ParentID = character.ID
                    };

                    addedCharacter.Items.Add(addedCharacterDeck);
                }

                TreeCharacters.Add(addedCharacter);
            }
        }

        #endregion

        #region Public methods

        public static PrintLayoutViewModel LoadViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore, SettingsStore settingsStore)
        {
            PrintLayoutViewModel viewModel = new(cardDesignerStore, navigationStore, settingsStore);
            viewModel.LoadData();

            return viewModel;
        }

        public void SetSelectedItem(TreeItemModel selectableItem)
        {
            // Set selected character
            if (selectableItem.ParentID == 0)
            {
                SelectedCharacter = AllCharacters.FirstOrDefault(c => c.ID == selectableItem.ID);
            }
            else
            {
                SelectedCharacter = AllCharacters.FirstOrDefault(c => c.ID == selectableItem.ParentID);
            }

            // Set character background deck
            SelectedCharacterBacgroundDesign = SelectedCharacter?.DeckBackgroundDesign ?? new();

            // Set selected deck
            List<ICard> cardsInDeck = new();
            if (selectableItem.Item is CharacterDeckModel characterCardModel)
            {
                SelectedCharacterDeck = characterCardModel;
                SelectedDeck = SelectedCharacterDeck;

                int deckDesingID = SelectedCharacter.CharacterDeckDescriptors.FirstOrDefault(idd => idd.Character.ID == SelectedCharacter.ID && idd.CharacterDeckID == SelectedCharacterDeck.ID)?.DesignID ?? 0;
                selectedCardDesign = AllCharacterDeckDesigns.FirstOrDefault(dd => dd.ID == deckDesingID) ?? new();

                cardsInDeck.AddRange(SelectedCharacterDeck.Cards);
            }

            if (selectableItem.Item is SpellDeckModel spellCardModel)
            {
                SelectedSpellDeck = spellCardModel;
                SelectedDeck = SelectedSpellDeck;

                int deckDesingID = SelectedCharacter.SpellDeckDescriptors.FirstOrDefault(idd => idd.Character.ID == SelectedCharacter.ID && idd.SpellDeckID == SelectedSpellDeck.ID)?.DesignID ?? 0;
                selectedCardDesign = AllSpellDeckDesigns.FirstOrDefault(dd => dd.ID == deckDesingID) ?? new();

                cardsInDeck.AddRange(SelectedSpellDeck.Cards);
            }

            if (selectableItem.Item is MinionDeckModel MinionCardModel)
            {
                SelectedMinionDeck = MinionCardModel;
                SelectedDeck = SelectedMinionDeck;

                int deckDesingID = SelectedCharacter.MinionDeckDescriptors.FirstOrDefault(idd => idd.Character.ID == SelectedCharacter.ID && idd.MinionDeckID == SelectedMinionDeck.ID)?.DesignID ?? 0;
                selectedCardDesign = AllMinionDeckDesigns.FirstOrDefault(dd => dd.ID == deckDesingID) ?? new();

                cardsInDeck.AddRange(SelectedMinionDeck.Cards);
            }

            if (selectableItem.Item is ItemDeckModel itemCardModel)
            {
                SelectedItemDeck = itemCardModel;
                SelectedDeck = SelectedItemDeck;

                int deckDesingID = SelectedCharacter.ItemDeckDescriptors.FirstOrDefault(idd => idd.Character.ID == SelectedCharacter.ID && idd.ItemDeckID == SelectedItemDeck.ID)?.DesignID ?? 0;
                selectedCardDesign = AllItemDeckDesigns.FirstOrDefault(dd => dd.ID == deckDesingID) ?? new();

                cardsInDeck.AddRange(SelectedItemDeck.Cards);
            }

            // Populate card pages
            int chunkSize = 9;
            int pageCtr = 1;
            int lastCount = cardsInDeck.Count % chunkSize;
            int nCards = cardsInDeck.Count;
            CardPages = new();
            for (int i = 0; i < nCards; i += chunkSize)
            {
                CardPageModel cardPage = new() { Name = $"Page {pageCtr}" };
                if (i + chunkSize > nCards)
                {
                    cardPage.Cards = cardsInDeck.GetRange(i, lastCount);
                }
                else
                {
                    cardPage.Cards = cardsInDeck.GetRange(i, chunkSize);
                }
                CardPages.Add(cardPage);
                pageCtr++;
            }

            // Populate background
            BackgroundPage = new()
            {
                new BackgroundCardModel(),
                new BackgroundCardModel(),
                new BackgroundCardModel(),
                new BackgroundCardModel(),
                new BackgroundCardModel(),
                new BackgroundCardModel(),
                new BackgroundCardModel(),
                new BackgroundCardModel(),
                new BackgroundCardModel(),
            };

            // Set first tab
            SelectedPageIndex = 0;
        }

        #endregion

        #region Settings

        partial void OnCardScaleChanged(double value)
        {
            SettingsStore.PrintCardScale = value;
        }

        partial void OnPageOffsetXChanged(double value)
        {
            SettingsStore.PrintPageOffsetX = value;
        }

        partial void OnPrintBacksideChanged(bool value)
        {
            SettingsStore.PrintBackside = value;
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
                            SelectedCharacter = AllCharacters.FirstOrDefault(ic => ic.ID == _navigationStore.SelectedCharacter.ID);
                            SelectedItemDeck = _navigationStore.SelectedItemDeck;
                            SelectedSpellDeck = _navigationStore.SelectedSpellDeck;
                            SelectedMinionDeck = _navigationStore.SelectedMinionDeck;
                            return;
                        default:
                            break;
                    }
                }
                else
                {
                    SelectedCharacter = AllCharacters.FirstOrDefault();
                }
            }
        }

        private void OnNavigatingAway(ViewModelType type)
        {
            SetUnsetDatabaseEvents(false);
        }

        #endregion
    }
}
