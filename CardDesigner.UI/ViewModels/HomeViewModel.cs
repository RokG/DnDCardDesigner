using CardDesigner.Domain.Enums;
using CardDesigner.Domain.HelperModels;
using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
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


        private List<ItemDeckDesignModel> AllItemDeckDesigns;
        private List<SpellDeckDesignModel> AllSpellDeckDesigns;
        private List<CharacterDeckDesignModel> AllCharacterDeckDesigns;

        #endregion

        #region Properties

        [ObservableProperty]
        private ObservableCollection<TreeItemModel> treeCharacters;

        [ObservableProperty]
        private ICardDesign selectedCardDesign;

        [ObservableProperty]
        private ICard selectedCard;

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
        public HomeViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            Name = Regex.Replace(nameof(HomeViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Home screen";
            Type = ViewModelType.Home;

            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;

            // TODO: is this OK? how is it different from old method (before MVVM toolkit)
            LoadData();
            GenerateCharacterTree();

        }

        #endregion

        #region Public methods

        public void SetSelectedItem(TreeItemModel selectableItem)
        {
            // Clear selection to trigger UI clear
            //SelectedCardDesign = null;
            //SelectedCard = null;

            if (selectableItem.Item is CharacterCardModel characterCardModel
                && selectableItem.Property is CharacterDeckDesignModel characterCardDesign)
            {
                SelectedCardDesign = characterCardDesign;
                SelectedCard = characterCardModel;
            }
            if (selectableItem.Item is SpellCardModel spellCardModel
                && selectableItem.Property is SpellDeckDesignModel spellCardDesign)
            {
                SelectedCardDesign = spellCardDesign;
                SelectedCard = spellCardModel;
            }
            if (selectableItem.Item is ItemCardModel itemCardModel
                && selectableItem.Property is ItemDeckDesignModel itemCardDesign)
            {
                SelectedCardDesign = itemCardDesign;
                SelectedCard = itemCardModel;
            }
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
                    Item = character,
                };

                // Create Item deck tree structure
                foreach (ItemDeckDesignLinkerModel itemDeckDescriptor in character.ItemDeckDescriptors)
                {
                    TreeItemModel addedItemDeck = new();
                    ItemDeckModel itemDeck = AllItemDecks.FirstOrDefault(id => id.ID == itemDeckDescriptor.ItemDeckID);
                    addedItemDeck.Name = itemDeck?.Name;
                    addedItemDeck.Title = itemDeck?.Title;
                    addedItemDeck.Item = itemDeck;

                    int deckDesingID = character.ItemDeckDescriptors.FirstOrDefault(idd => idd.Character.ID == character.ID && idd.ItemDeckID == itemDeck.ID)?.ID ?? 0;
                    ItemDeckDesignModel deckDesignModel = AllItemDeckDesigns.FirstOrDefault(dd => dd.ID == deckDesingID);

                    foreach (ItemCardModel itemCard in itemDeck.ItemCards)
                    {
                        TreeItemModel addedItemCard = new()
                        {
                            Name = itemCard?.Name,
                            Title = itemCard?.Title,
                            Item = itemCard,
                            Property = deckDesignModel
                        };
                        addedItemDeck.Items.Add(addedItemCard);
                    }
                    addedCharacter.Items.Add(addedItemDeck);
                }

                // Create Spell deck tree structure
                foreach (SpellDeckDesignLinkerModel spellDeckDescriptor in character.SpellDeckDescriptors)
                {
                    TreeItemModel addedSpellDeck = new();
                    SpellDeckModel spellDeck = AllSpellDecks.FirstOrDefault(id => id.ID == spellDeckDescriptor.SpellDeckID);
                    addedSpellDeck.Name = spellDeck?.Name;
                    addedSpellDeck.Title = spellDeck?.Title;

                    int deckDesingID = character.SpellDeckDescriptors.FirstOrDefault(idd => idd.Character.ID == character.ID && idd.SpellDeckID == spellDeck.ID)?.ID ?? 0;
                    SpellDeckDesignModel deckDesignModel = AllSpellDeckDesigns.FirstOrDefault(dd => dd.ID == deckDesingID);

                    foreach (SpellCardModel spellCard in spellDeck.SpellCards)
                    {
                        TreeItemModel addedSpellCard = new()
                        {
                            Name = spellCard?.Name,
                            Title = spellCard?.Title,
                            Item = spellCard,
                            Property = deckDesignModel
                        };
                        addedSpellDeck.Items.Add(addedSpellCard);
                    }
                    addedCharacter.Items.Add(addedSpellDeck);
                }

                // Create Character deck tree structure
                foreach (CharacterDeckDesignLinkerModel characterDeckDescriptor in character.CharacterDeckDescriptors)
                {
                    TreeItemModel addedCharacterDeck = new();
                    CharacterDeckModel characterDeck = AllCharacterDecks.FirstOrDefault(id => id.ID == characterDeckDescriptor.CharacterDeckID);
                    addedCharacterDeck.Name = characterDeck?.Name;
                    addedCharacterDeck.Title = characterDeck?.Title;

                    int deckDesingID = character.CharacterDeckDescriptors.FirstOrDefault(idd => idd.Character.ID == character.ID && idd.CharacterDeckID == characterDeck.ID)?.ID ?? 0;
                    CharacterDeckDesignModel deckDesignModel = AllCharacterDeckDesigns.FirstOrDefault(dd => dd.ID == deckDesingID);

                    foreach (CharacterCardModel characterCard in characterDeck.CharacterCards)
                    {
                        TreeItemModel addedCharacterCard = new()
                        {
                            Name = characterCard?.Name,
                            Title = characterCard?.Title,
                            Item = characterCard,
                            Property = deckDesignModel
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

        public static HomeViewModel LoadViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            return new(cardDesignerStore, navigationStore);
        }

        #endregion
    }

}
