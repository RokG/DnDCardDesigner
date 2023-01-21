using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace CardDesigner.UI.ViewModels
{
    public partial class HomeViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;

        #endregion

        #region Properties

        [ObservableProperty]
        private ObservableCollection<SpellCardModel> allSpellCards;

        [ObservableProperty]
        private ObservableCollection<SpellDeckModel> allSpellDecks;

        [ObservableProperty]
        private ObservableCollection<ItemCardModel> allItemCards;

        [ObservableProperty]
        private ObservableCollection<ItemDeckModel> allItemDecks;

        [ObservableProperty]
        private ObservableCollection<CharacterModel> allCharacters;

        #endregion

        #region Constructor
        public HomeViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = Regex.Replace(nameof(HomeViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Home screen";

            _cardDesignerStore = cardDesignerStore;

            // TODO: is this OK? how is it different from old method (before MVVM toolkit)
            LoadData();
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
        }

        #endregion

        #region Public methods

        public static HomeViewModel LoadViewModel(CardDesignerStore cardDesignerStore)
        {
            return new(cardDesignerStore);
        }

        #endregion
    }

}
