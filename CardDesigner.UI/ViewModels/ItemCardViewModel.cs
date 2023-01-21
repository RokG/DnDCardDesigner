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
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public partial class ItemCardViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;

        #endregion

        #region Properties

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateItemCardCommand))]
        private string itemCardName;

        [ObservableProperty]
        private ItemCardModel selectedItemCard;

        [ObservableProperty]
        private ObservableCollection<ItemCardModel> allItemCards;

        #endregion

        #region Actions, Events, Commands

        public ICommand DoNavigateCommand { get; }

        #endregion Actions, Events, Commands

        #region Constructor

        public ItemCardViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = Regex.Replace(nameof(ItemCardViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Create, view and edit Item Cards";

            _cardDesignerStore = cardDesignerStore;

            _cardDesignerStore.ItemCardCreated += OnSpellCardCreated;

            // TODO: is this OK? how is it different from old method (before MVVM toolkit)
            LoadData();
        }

        private void OnSpellCardCreated(ItemCardModel itemCard)
        {
            AllItemCards.Add(itemCard);
            SelectedItemCard = itemCard;
        }

        #endregion

        #region Private methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllItemCards = new(_cardDesignerStore.ItemCards);
        }

        #endregion

        #region Public methods

        public static ItemCardViewModel LoadViewModel(CardDesignerStore cardDesignerStore)
        {
            ItemCardViewModel viewModel = new(cardDesignerStore);
            viewModel.LoadData();

            return viewModel;
        }

        #endregion

        #region Commands

        [RelayCommand(CanExecute = nameof(CanCreateItemCard))]
        private async void CreateItemCard()
        {
            await _cardDesignerStore.CreateItemCard(new ItemCardModel() { Name = ItemCardName });
        }

        private bool CanCreateItemCard()
        {
            return ItemCardName != null
                && ItemCardName != string.Empty
                && !AllItemCards.Where(c => c.Name == ItemCardName).Any();
        }

        [RelayCommand]
        private async void UpdateItemCard()
        {
            await _cardDesignerStore.UpdateItemCard(SelectedItemCard);
        }

        #endregion
    }
}