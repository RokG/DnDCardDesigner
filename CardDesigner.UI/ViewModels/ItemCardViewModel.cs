using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public partial class ItemCardViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;
        private readonly NavigationStore _navigationStore;

        #endregion

        #region Properties

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateItemCardCommand))]
        private string itemCardName;

        [ObservableProperty]
        private ICollectionView allArmoursCollectionView;

        [ObservableProperty]
        private ICollectionView allWeaponsCollectionView;

        [ObservableProperty]
        private ItemCardModel selectedItemCard;

        [ObservableProperty]
        private string armourSearchFilter;
        [ObservableProperty]
        private string weaponSearchFilter;

        [ObservableProperty]
        private ObservableCollection<ItemCardModel> allItemCards;

        [ObservableProperty]
        private ObservableCollection<WeaponModel> allWeapons;

        [ObservableProperty]
        private ObservableCollection<ArmourModel> allArmours;

        #endregion

        #region Actions, Events, Commands

        public ICommand DoNavigateCommand { get; }

        #endregion Actions, Events, Commands

        #region Constructor

        public ItemCardViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            Name = Regex.Replace(nameof(ItemCardViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Create, view and edit Item Cards";
            Type = ViewModelType.ItemCardCreator;

            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;

            _cardDesignerStore.ItemCardChanged += OnSpellCardChanged;

            LoadData();

            allArmoursCollectionView = CollectionViewSource.GetDefaultView(AllArmours);
            allWeaponsCollectionView = CollectionViewSource.GetDefaultView(AllWeapons);
        }

        partial void OnArmourSearchFilterChanged(string bookingDate)
        {
            AllArmoursCollectionView.Filter = new Predicate<object>(ArmourFilter);
        }

        partial void OnWeaponSearchFilterChanged(string bookingDate)
        {
            AllWeaponsCollectionView.Filter = new Predicate<object>(WeaponFilter);
        }

        private bool ArmourFilter(object obj)
        {
            //your logicComplexFilter
            ArmourModel arm = (ArmourModel)obj;
            return
                ArmourSearchFilter == null ? false :
               arm.Name.Contains(ArmourSearchFilter, StringComparison.OrdinalIgnoreCase)
            || arm.EquipmentSlot.ToString().Contains(ArmourSearchFilter, StringComparison.OrdinalIgnoreCase)
            || arm.ArmourClass.ToString().Contains(ArmourSearchFilter, StringComparison.OrdinalIgnoreCase)
            || arm.ArmourType.ToString().Contains(ArmourSearchFilter, StringComparison.OrdinalIgnoreCase);
        }

        private bool WeaponFilter(object obj)
        {
            //your logicComplexFilter
            WeaponModel weapon = (WeaponModel)obj;
            return
                WeaponSearchFilter == null ? false :
               weapon.Name.Contains(WeaponSearchFilter, StringComparison.OrdinalIgnoreCase)
            || weapon.EquipmentSlot.ToString().Contains(WeaponSearchFilter, StringComparison.OrdinalIgnoreCase)
            || weapon.WeaponType.ToString().Contains(WeaponSearchFilter, StringComparison.OrdinalIgnoreCase)
            || weapon.DiceType.ToString().Contains(WeaponSearchFilter, StringComparison.OrdinalIgnoreCase)
            || weapon.PhysicalDamageType.ToString().Contains(WeaponSearchFilter, StringComparison.OrdinalIgnoreCase)
            || weapon.DamageModifier.ToString().Contains(WeaponSearchFilter, StringComparison.OrdinalIgnoreCase);
        }

        private void OnSpellCardChanged(ItemCardModel itemCard, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllItemCards.Add(itemCard);
                    SelectedItemCard = itemCard;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Private methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllItemCards = new(_cardDesignerStore.ItemCards);
            AllArmours = new(_cardDesignerStore.Armours);
            AllWeapons = new(_cardDesignerStore.Weapons);

            SelectedItemCard = AllItemCards.FirstOrDefault();
        }

        #endregion

        #region Public methods

        public static ItemCardViewModel LoadViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            ItemCardViewModel viewModel = new(cardDesignerStore, navigationStore);
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
            SelectedItemCard.ItemID = SelectedItemCard.Item?.ID;
            await _cardDesignerStore.UpdateItemCard(SelectedItemCard);
        }

        #endregion
    }
}