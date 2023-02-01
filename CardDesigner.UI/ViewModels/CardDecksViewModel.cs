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

            LoadData();

            SetSelectionFromNavigation();

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
                        SelectedItemDeck= _navigationStore.SelectedItemDeck;
                        return;
                    default:
                        break;
                }
            }
            SelectedSpellDeck = AllSpellDecks.FirstOrDefault();
            SelectedItemDeck = AllItemDecks.FirstOrDefault();
        }

        #endregion

        #region Database update methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllSpellCards = new(_cardDesignerStore.SpellCards);
            AllSpellDecks = new(_cardDesignerStore.SpellDecks);
            AllItemCards = new(_cardDesignerStore.ItemCards);
            AllItemDecks = new(_cardDesignerStore.ItemDecks);
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

        #endregion

        #region Public methods

        public static CardDecksViewModel LoadViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            CardDecksViewModel viewModel = new(cardDesignerStore, navigationStore);
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
