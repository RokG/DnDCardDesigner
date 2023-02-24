using CardDesigner.Domain.Enums;
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
        private int cardSize = 698;

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

        #region MyRegion

        public PrintLayoutViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            Name = Regex.Replace(nameof(PrintLayoutViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Print layout";
            Type = ViewModelType.PrintLayout;

            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;

            LoadData();

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

        #endregion

        #region Public methods

        public static PrintLayoutViewModel LoadViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            PrintLayoutViewModel viewModel = new(cardDesignerStore, navigationStore);
            viewModel.LoadData();

            return viewModel;
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
