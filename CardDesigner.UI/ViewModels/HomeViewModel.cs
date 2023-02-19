using CardDesigner.Domain.Enums;
using CardDesigner.Domain.HelperModels;
using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
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

        #endregion

        #region Properties

        [ObservableProperty]
        private ObservableCollection<TreeItemModel> treeCharacters;

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

        public void SetSelectedItem(object selectableItem)
        {
            if (selectableItem is CharacterCardModel characterCardModel)
            {

            }
            if (selectableItem is SpellCardModel spellCardModel)
            {

            }
            if (selectableItem is ItemCardModel itemCardModel)
            {

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
                    Item= character,
                };

                // Create Item deck tree structure
                foreach (ItemDeckDesignLinkerModel itemDeckDescriptor in character.ItemDeckDescriptors)
                {
                    TreeItemModel addedItemDeck = new();
                    ItemDeckModel itemDeck = AllItemDecks.FirstOrDefault(id => id.ID == itemDeckDescriptor.ItemDeckID);
                    addedItemDeck.Name = itemDeck?.Name;
                    addedItemDeck.Title = itemDeck?.Title;
                    addedItemDeck.Item = itemDeck;

                    foreach (ItemCardModel itemCard in itemDeck.ItemCards)
                    {
                        TreeItemModel addedItemCard = new()
                        {
                            Name = itemCard?.Name,
                            Title = itemCard?.Title,
                            Item = itemCard
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

                    foreach (SpellCardModel spellCard in spellDeck.SpellCards)
                    {
                        TreeItemModel addedSpellCard = new()
                        {
                            Name = spellCard?.Name,
                            Title = spellCard?.Title
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

                    foreach (CharacterCardModel characterCard in characterDeck.CharacterCards)
                    {
                        TreeItemModel addedCharacterCard = new()
                        {
                            Name = characterCard?.Name,
                            Title = characterCard?.Title
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
