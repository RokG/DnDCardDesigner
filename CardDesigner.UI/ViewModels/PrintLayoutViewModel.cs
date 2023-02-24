using CardDesigner.Domain.Enums;
using CardDesigner.Domain.HelperModels;
using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace CardDesigner.UI.ViewModels
{
    public partial class PrintLayoutViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;
        private readonly NavigationStore _navigationStore;

        private List<ItemDeckDesignModel> AllItemDeckDesigns;
        private List<SpellDeckDesignModel> AllSpellDeckDesigns;
        private List<CharacterDeckDesignModel> AllCharacterDeckDesigns;

        #endregion

        #region Properties

        [ObservableProperty]
        private ObservableCollection<ObservableCollection<ICard>> cardPages;

        [ObservableProperty]
        private ObservableCollection<TreeItemModel> treeCharacters;

        [ObservableProperty]
        private int cardSize = 698;

        [ObservableProperty]
        private int selectedPageIndex = 0;

        [ObservableProperty]
        private ICardDesign selectedCardDesign;

        [ObservableProperty]
        private ICard selectedCard;

        [ObservableProperty]
        private IDeck selectedDeck;

        [ObservableProperty]
        private ItemDeckModel selectedItemDeck;

        [ObservableProperty]
        private SpellDeckModel selectedSpellDeck;

        [ObservableProperty]
        private CharacterDeckModel selectedCharacterDeck;

        [ObservableProperty]
        private CharacterModel selectedCharacter;

        [ObservableProperty]
        private ObservableCollection<SpellDeckModel> allSpellDecks;

        [ObservableProperty]
        private ObservableCollection<ItemDeckModel> allItemDecks;

        [ObservableProperty]
        private ObservableCollection<CharacterDeckModel> allCharacterDecks;

        [ObservableProperty]
        private ObservableCollection<CharacterModel> allCharacters;

        #endregion

        #region Constructor

        public PrintLayoutViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            Name = Regex.Replace(nameof(PrintLayoutViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Print layout";
            Type = ViewModelType.PrintLayout;

            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;

            LoadData();

            GenerateCharacterTree();
        }

        #endregion

        #region Private methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllCharacters = _cardDesignerStore.Characters == null ? new() : new(_cardDesignerStore.Characters);
            AllSpellDecks = _cardDesignerStore.SpellDecks == null ? new() : new(_cardDesignerStore.SpellDecks);
            AllItemDecks = _cardDesignerStore.ItemDecks == null ? new() : new(_cardDesignerStore.ItemDecks);
            AllCharacterDecks = _cardDesignerStore.CharacterDecks == null ? new() : new(_cardDesignerStore.CharacterDecks);

            SelectedItemDeck = AllItemDecks.FirstOrDefault();
            SelectedSpellDeck = AllSpellDecks.FirstOrDefault();
            SelectedCharacterDeck = AllCharacterDecks.FirstOrDefault();

            AllItemDeckDesigns = _cardDesignerStore.ItemDeckDesigns.ToList();
            AllSpellDeckDesigns = _cardDesignerStore.SpellDeckDesigns.ToList();
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

        public static PrintLayoutViewModel LoadViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            PrintLayoutViewModel viewModel = new(cardDesignerStore, navigationStore);
            viewModel.LoadData();

            return viewModel;
        }

        public void SetSelectedItem(TreeItemModel selectableItem)
        {
            List<ICard> cardsInDeck = new();
            if (selectableItem.Item is CharacterDeckModel characterCardModel)
            {
                SelectedCharacterDeck = characterCardModel;
                cardsInDeck.AddRange(SelectedCharacterDeck.CharacterCards);
            }

            if (selectableItem.Item is SpellDeckModel spellCardModel)
            {
                SelectedSpellDeck = spellCardModel;
                cardsInDeck.AddRange(SelectedSpellDeck.SpellCards);
            }

            if (selectableItem.Item is ItemDeckModel itemCardModel)
            {
                SelectedItemDeck = itemCardModel;
                cardsInDeck.AddRange(SelectedItemDeck.ItemCards);
            }

            int chunkSize = 9;
            IEnumerable<IEnumerable<ICard>> CardPagesList = cardsInDeck
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value));

            CardPages = new();
            foreach (IEnumerable<ICard> item in CardPagesList)
            {
                CardPages.Add(new(item));
            }
            SelectedPageIndex = 0;
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
