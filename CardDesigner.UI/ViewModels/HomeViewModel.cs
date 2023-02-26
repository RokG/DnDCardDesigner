using CardDesigner.Domain.Enums;
using CardDesigner.Domain.HelperModels;
using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace CardDesigner.UI.ViewModels
{
    public partial class HomeViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;
        private readonly NavigationStore _navigationStore;
        private readonly SettingsStore _settingsStore;

        private List<ItemDeckDesignModel> AllItemDeckDesigns;
        private List<SpellDeckDesignModel> AllSpellDeckDesigns;
        private List<CharacterDeckDesignModel> AllCharacterDeckDesigns;
        private CardType selectedCardType;

        #endregion

        #region Properties

        [ObservableProperty]
        private ObservableCollection<TreeItemModel> treeCharacters;

        [ObservableProperty]
        private ICardDesign selectedCardDesign;

        [ObservableProperty]
        private ICard selectedCard;

        [ObservableProperty]
        private IDeck selectedDeck;

        [ObservableProperty]
        private CharacterModel selectedCharacter;

        [ObservableProperty]
        private ObservableCollection<SpellCardModel> allSpellCards;

        [ObservableProperty]
        private ObservableCollection<SpellDeckModel> allSpellDecks;

        [ObservableProperty]
        private ObservableCollection<ItemCardModel> allItemCards;

        [ObservableProperty]
        private ObservableCollection<ItemDeckModel> allItemDecks;

        [ObservableProperty]
        private ObservableCollection<CharacterDeckModel> allCharacterDecks;

        [ObservableProperty]
        private ObservableCollection<CharacterCardModel> allCharacterCards;

        [ObservableProperty]
        private ObservableCollection<CharacterModel> allCharacters;

        #endregion

        #region Constructor

        public HomeViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore, SettingsStore settingsStore)
        {
            Name = Regex.Replace(nameof(HomeViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Home screen";
            Type = ViewModelType.Home;

            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;
            _settingsStore = settingsStore;

            LoadData();
            GenerateCharacterTree();
        }

        #endregion

        #region Private methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllCharacters = _cardDesignerStore.Characters == null ? new() : new(_cardDesignerStore.Characters);
            AllSpellCards = _cardDesignerStore.SpellCards == null ? new() : new(_cardDesignerStore.SpellCards);
            AllSpellDecks = _cardDesignerStore.SpellDecks == null ? new() : new(_cardDesignerStore.SpellDecks);
            AllItemCards = _cardDesignerStore.ItemCards == null ? new() : new(_cardDesignerStore.ItemCards);
            AllItemDecks = _cardDesignerStore.ItemDecks == null ? new() : new(_cardDesignerStore.ItemDecks);
            AllCharacterCards = _cardDesignerStore.CharacterCards == null ? new() : new(_cardDesignerStore.CharacterCards);
            AllCharacterDecks = _cardDesignerStore.CharacterDecks == null ? new() : new(_cardDesignerStore.CharacterDecks);

            AllItemDeckDesigns = _cardDesignerStore.ItemDeckDesigns.ToList();
            AllSpellDeckDesigns = _cardDesignerStore.SpellDeckDesigns.ToList();
            AllCharacterDeckDesigns = _cardDesignerStore.CharacterDeckDesigns.ToList();
        }

        private void SetSelectedItemParents(TreeItemModel treeItemModel, CardType itemType)
        {
            SelectedCharacter = AllCharacters.FirstOrDefault(c => c.ID == treeItemModel.GrandParentID);
            int deckDesignID = 0;
            if (treeItemModel.Item != null)
            {
                switch (itemType)
                {
                    case CardType.Spell:
                        SelectedDeck = AllSpellDecks.FirstOrDefault(d => d.ID == treeItemModel.ParentID);
                        deckDesignID = SelectedCharacter.SpellDeckDescriptors
                            .FirstOrDefault(dd =>
                            dd.SpellDeckID == treeItemModel.ParentID
                            && dd.Character.ID == SelectedCharacter.ID)
                            .DesignID;
                        SelectedCardDesign = AllSpellDeckDesigns.FirstOrDefault(dd => dd.ID == deckDesignID) ?? new();
                        break;
                    case CardType.Item:
                        SelectedDeck = AllItemDecks.FirstOrDefault(d => d.ID == treeItemModel.ParentID);
                        deckDesignID = SelectedCharacter.ItemDeckDescriptors
                            .FirstOrDefault(dd =>
                            dd.ItemDeckID == treeItemModel.ParentID
                            && dd.Character.ID == SelectedCharacter.ID)
                            .DesignID;
                        SelectedCardDesign = AllItemDeckDesigns.FirstOrDefault(dd => dd.ID == deckDesignID) ?? new();
                        break;
                    case CardType.Character:
                        SelectedDeck = AllCharacterDecks.FirstOrDefault(d => d.ID == treeItemModel.ParentID);
                        deckDesignID = SelectedCharacter.CharacterDeckDescriptors
                            .FirstOrDefault(dd =>
                            dd.CharacterDeckID == treeItemModel.ParentID
                            && dd.Character.ID == SelectedCharacter.ID)
                            .DesignID;
                        SelectedCardDesign = AllCharacterDeckDesigns.FirstOrDefault(dd => dd.ID == deckDesignID) ?? new();
                        break;
                    default:
                        break;
                }
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
                        ParentID = character.ID
                    };

                    foreach (ItemCardModel itemCard in itemDeck.ItemCards)
                    {
                        TreeItemModel addedItemCard = new()
                        {
                            Name = itemCard?.Name,
                            Title = itemCard?.Title,
                            ID = itemCard.ID,
                            ParentID = itemDeck.ID,
                            GrandParentID = character.ID,
                            Item = itemCard,
                        };
                        addedItemDeck.Items.Add(addedItemCard);
                    }
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
                        ParentID = character.ID
                    };

                    foreach (SpellCardModel spellCard in spellDeck.SpellCards)
                    {
                        TreeItemModel addedSpellCard = new()
                        {
                            Name = spellCard?.Name,
                            Title = spellCard?.Title,
                            ID = spellCard.ID,
                            ParentID = addedSpellDeck.ID,
                            GrandParentID = character.ID,
                            Item = spellCard,
                        };
                        addedSpellDeck.Items.Add(addedSpellCard);
                    }
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
                        ParentID = character.ID
                    };

                    foreach (CharacterCardModel characterCard in characterDeck.CharacterCards)
                    {
                        TreeItemModel addedCharacterCard = new()
                        {
                            Name = characterCard?.Name,
                            Title = characterCard?.Title,
                            ID = characterCard.ID,
                            ParentID = characterDeck.ID,
                            GrandParentID = character.ID,
                            Item = characterCard,
                        };
                        addedCharacterDeck.Items.Add(addedCharacterCard);
                    }
                    addedCharacter.Items.Add(addedCharacterDeck);
                }

                TreeCharacters.Add(addedCharacter);
            }
        }

        #endregion

        #region Public methods

        public static HomeViewModel LoadViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore, SettingsStore settingsStore)
        {
            return new(cardDesignerStore, navigationStore, settingsStore);
        }

        public void SetSelectedItem(TreeItemModel selectableItem)
        {
            if (selectableItem.Item is CharacterCardModel characterCardModel)
            {
                SelectedCard = characterCardModel;
                selectedCardType = CardType.Character;
            }

            if (selectableItem.Item is SpellCardModel spellCardModel)
            {
                SelectedCard = spellCardModel;
                selectedCardType = CardType.Spell;
            }

            if (selectableItem.Item is ItemCardModel itemCardModel)
            {
                SelectedCard = itemCardModel;
                selectedCardType = CardType.Item;
            }

            SetSelectedItemParents(selectableItem, selectedCardType);
        }

        #endregion

        #region Commands

        [RelayCommand]
        private void NavigateToCardCreator()
        {
            switch (SelectedCard)
            {
                case ItemCardModel itemCard:
                    _navigationStore.SelectedItemCard = itemCard;
                    _navigationStore.SelectedItemDeckDesign = (ItemDeckDesignModel)SelectedCardDesign;
                    _navigationStore.NavigateTo(ViewModelType.ItemCardCreator);
                    break;
                case SpellCardModel spellCard:
                    _navigationStore.SelectedSpellCard = spellCard;
                    _navigationStore.SelectedSpellDeckDesign = (SpellDeckDesignModel)SelectedCardDesign;
                    _navigationStore.NavigateTo(ViewModelType.SpellCardCreator);
                    break;
                case CharacterCardModel characterCard:
                    _navigationStore.SelectedCharacterCard = characterCard;
                    _navigationStore.SelectedCharacterDeckDesign = (CharacterDeckDesignModel)SelectedCardDesign;
                    _navigationStore.NavigateTo(ViewModelType.CharacterCardCreator);
                    break;
                default:
                    break;
            }
        }

        [RelayCommand]
        private void NavigateToDeckCreator()
        {
            switch (selectedCardType)
            {
                case CardType.Spell:
                    _navigationStore.SelectedSpellDeck = (SpellDeckModel)SelectedDeck;
                    break;
                case CardType.Item:
                    _navigationStore.SelectedItemDeck = (ItemDeckModel)SelectedDeck;
                    break;
                case CardType.Character:
                    _navigationStore.SelectedCharacterDeck = (CharacterDeckModel)SelectedDeck;
                    break;
                default:
                    break;
            }
            _navigationStore.SelectedCharacter = SelectedCharacter;
            _navigationStore.SelectedCardType = selectedCardType;
            _navigationStore.NavigateTo(ViewModelType.DeckCreator);
        }

        [RelayCommand]
        private void NavigateToDeckDesign()
        {
            switch (selectedCardType)
            {
                case CardType.Spell:
                    _navigationStore.SelectedSpellDeck = (SpellDeckModel)SelectedDeck;
                    _navigationStore.SelectedSpellCard = (SpellCardModel)SelectedCard;
                    _navigationStore.SelectedSpellDeckDesign = (SpellDeckDesignModel)SelectedCardDesign;
                    break;
                case CardType.Item:
                    _navigationStore.SelectedItemDeck = (ItemDeckModel)SelectedDeck;
                    _navigationStore.SelectedItemCard = (ItemCardModel)SelectedCard;
                    _navigationStore.SelectedItemDeckDesign = (ItemDeckDesignModel)SelectedCardDesign;
                    break;
                case CardType.Character:
                    _navigationStore.SelectedCharacterDeck = (CharacterDeckModel)SelectedDeck;
                    _navigationStore.SelectedCharacterCard = (CharacterCardModel)SelectedCard;
                    _navigationStore.SelectedCharacterDeckDesign = (CharacterDeckDesignModel)SelectedCardDesign;
                    break;
                default:
                    break;
            }
            _navigationStore.SelectedCharacter = SelectedCharacter;
            _navigationStore.SelectedCardType = selectedCardType;
            _navigationStore.NavigateTo(ViewModelType.DeckDesigner);
        }

        [RelayCommand]
        private void NavigateToPrintView()
        {
            switch (selectedCardType)
            {
                case CardType.Spell:
                    _navigationStore.SelectedSpellDeck = (SpellDeckModel)SelectedDeck;
                    _navigationStore.SelectedSpellCard = (SpellCardModel)SelectedCard;
                    _navigationStore.SelectedSpellDeckDesign = (SpellDeckDesignModel)SelectedCardDesign;
                    break;
                case CardType.Item:
                    _navigationStore.SelectedItemDeck = (ItemDeckModel)SelectedDeck;
                    _navigationStore.SelectedItemCard = (ItemCardModel)SelectedCard;
                    _navigationStore.SelectedItemDeckDesign = (ItemDeckDesignModel)SelectedCardDesign;
                    break;
                case CardType.Character:
                    _navigationStore.SelectedCharacterDeck = (CharacterDeckModel)SelectedDeck;
                    _navigationStore.SelectedCharacterCard = (CharacterCardModel)SelectedCard;
                    _navigationStore.SelectedCharacterDeckDesign = (CharacterDeckDesignModel)SelectedCardDesign;
                    break;
                default:
                    break;
            }
            _navigationStore.SelectedCharacter = SelectedCharacter;
            _navigationStore.SelectedCardType = selectedCardType;
            _navigationStore.NavigateTo(ViewModelType.PrintLayout);
        }

        #endregion
    }
}
