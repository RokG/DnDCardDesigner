using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace CardDesigner.UI.ViewModels
{
    public partial class CardDecksViewModel : ViewModelBase
    {

        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;

        #endregion

        #region Properties

        [ObservableProperty]
        private ICard selectedCard;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateSpellDeckCommand))]
        private string addedSpellDeckName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateItemDeckCommand))]
        private string addedItemDeckName;

        [ObservableProperty]
        private string addedSpellCardName;
        [ObservableProperty]
        private string addedItemCardName;

        [ObservableProperty]
        private CharacterModel selectedCharacter;

        [ObservableProperty]
        private SpellDeckModel selectedSpellDeck;
        [ObservableProperty]
        private ItemDeckModel selectedItemDeck;

        [ObservableProperty]
        private SpellCardModel selectedSpellCard;
        [ObservableProperty]
        private ItemCardModel selectedItemCard;

        [ObservableProperty]
        private ObservableCollection<SpellCardModel> allSpellCards;
        [ObservableProperty]
        private ObservableCollection<ItemCardModel> allItemCards;

        [ObservableProperty]
        private ObservableCollection<SpellDeckModel> allSpellDecks;
        [ObservableProperty]
        private ObservableCollection<ItemDeckModel> allItemDecks;

        [ObservableProperty]
        private ObservableCollection<CharacterModel> allCharacters;

        #endregion Properties

        #region Constructor

        public CardDecksViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = Regex.Replace(nameof(CardDecksViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Create, view and edit Spell Decks";

            _cardDesignerStore = cardDesignerStore;

            _cardDesignerStore.CharacterCreated += OnCharacterCreated;
            _cardDesignerStore.CharacterDeleted += OnCharacterDeleted;

            _cardDesignerStore.SpellDeckCreated += OnSpellDeckCreated;
            _cardDesignerStore.SpellDeckUpdated += OnSpellDeckUpdated;
            _cardDesignerStore.SpellDeckDeleted += OnSpellDeckDeleted;
            _cardDesignerStore.SpellCardCreated += OnSpellCardCreated;

            _cardDesignerStore.ItemDeckCreated += OnItemDeckCreated;
            _cardDesignerStore.ItemDeckUpdated += OnItemDeckUpdated;
            _cardDesignerStore.ItemDeckDeleted += OnItemDeckDeleted;
            _cardDesignerStore.ItemCardCreated += OnItemCardCreated;

            // TODO: is this OK? how is it different from old method (before MVVM toolkit)
            LoadData();
        }

        #endregion

        #region Private methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllCharacters = new(_cardDesignerStore.Characters);
            AllSpellCards = new(_cardDesignerStore.SpellCards);
            AllSpellDecks = new(_cardDesignerStore.SpellDecks);
            AllItemCards = new(_cardDesignerStore.ItemCards);
            AllItemDecks = new(_cardDesignerStore.ItemDecks);
        }

        private void OnCharacterCreated(CharacterModel character)
        {
            AllCharacters.Add(character);
            SelectedCharacter = character;
        }

        private void OnCharacterDeleted(CharacterModel character)
        {
            AllCharacters.Remove(SelectedCharacter);
            SelectedCharacter = AllCharacters.FirstOrDefault();
        }

        private void OnSpellDeckCreated(SpellDeckModel spellDeck)
        {
            AllSpellDecks.Add(spellDeck);
            SelectedSpellDeck = spellDeck;
        }

        private void OnSpellDeckUpdated(SpellDeckModel spellDeck)
        {
            SelectedSpellDeck = spellDeck;
        }

        private void OnSpellDeckDeleted(SpellDeckModel spellDeck)
        {
            AllSpellDecks.Remove(SelectedSpellDeck);
            SelectedSpellDeck = AllSpellDecks.FirstOrDefault();
        }

        private void OnSpellCardCreated(SpellCardModel spellCard)
        {
            AllSpellCards.Add(spellCard);
            SelectedSpellCard = spellCard;
        }

        private void OnItemDeckCreated(ItemDeckModel itemDeck)
        {
            AllItemDecks.Add(itemDeck);
            SelectedItemDeck = itemDeck;
        }

        private void OnItemDeckUpdated(ItemDeckModel itemDeck)
        {
            SelectedItemDeck = itemDeck;
        }

        private void OnItemDeckDeleted(ItemDeckModel itemDeck)
        {
            AllItemDecks.Remove(SelectedItemDeck);
            SelectedItemDeck = AllItemDecks.FirstOrDefault();
        }

        private void OnItemCardCreated(ItemCardModel itemCard)
        {
            AllItemCards.Add(itemCard);
            SelectedItemCard = itemCard;
        }

        #endregion

        #region Public methods

        public static CardDecksViewModel LoadViewModel(CardDesignerStore cardDesignerStore)
        {
            CardDecksViewModel viewModel = new(cardDesignerStore);
            viewModel.LoadData();

            return viewModel;
        }

        #endregion

        #region Commands

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
        private async void UpdateCharacter()
        {
            SelectedCharacter.SpellDeck = SelectedSpellDeck;
            SelectedCharacter.ItemDeck = SelectedItemDeck;
            await _cardDesignerStore.UpdateCharacter(SelectedCharacter);
        }

        [RelayCommand]
        private async void AddSpellCardToDeck(SpellCardModel spellCard)
        {
            SelectedSpellDeck.SpellCards.Add(spellCard);
            await _cardDesignerStore.UpdateSpellDeck(SelectedSpellDeck);
        }

        [RelayCommand]
        private async void AddItemCardToDeck(ItemCardModel itemCard)
        {
            SelectedItemDeck.ItemCards.Add(itemCard);
            await _cardDesignerStore.UpdateItemDeck(SelectedItemDeck);
        }

        [RelayCommand]
        private async void RemoveSpellCardFromDeck(SpellCardModel spellCard)
        {
            SelectedSpellDeck.SpellCards.Remove(spellCard);
            await _cardDesignerStore.UpdateSpellDeck(SelectedSpellDeck);
        }

        [RelayCommand]
        private async void RemoveItemCardFromDeck(ItemCardModel itemCard)
        {
            SelectedItemDeck.ItemCards.Remove(itemCard);
            await _cardDesignerStore.UpdateItemDeck(SelectedItemDeck);
        }
        [RelayCommand]
        private async void DeleteSpellDeck()
        {
            await _cardDesignerStore.DeleteSpellDeck(SelectedSpellDeck);
        }

        [RelayCommand]
        private async void DeleteItemDeck()
        {
            await _cardDesignerStore.DeleteItemDeck(SelectedItemDeck);
        }
        #endregion
    }
}
