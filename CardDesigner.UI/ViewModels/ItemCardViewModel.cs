using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Enums;
using CardDesigner.Domain.HelperModels;
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
        private ICollectionView allUsablesCollectionView;

        [ObservableProperty]
        private ICollectionView allClothingCollectionView;

        [ObservableProperty]
        private ICollectionView allConsumablesCollectionView;

        [ObservableProperty]
        private ItemCardModel selectedItemCard;

        [ObservableProperty]
        private WeaponModel selectedWeapon;

        [ObservableProperty]
        private ArmourModel selectedArmour;

        [ObservableProperty]
        private string armourSearchFilter;

        [ObservableProperty]
        private string weaponSearchFilter;

        [ObservableProperty]
        private string usableSearchFilter;

        [ObservableProperty]
        private string clothingSearchFilter;

        [ObservableProperty]
        private string consumableSearchFilter;

        [ObservableProperty]
        private ItemDeckDesignModel selectedItemDeckDesign = new();

        [ObservableProperty]
        private ObservableCollection<ItemCardModel> allItemCards;

        [ObservableProperty]
        private ObservableCollection<WeaponModel> allWeapons;

        [ObservableProperty]
        private ObservableCollection<ArmourModel> allArmours;

        [ObservableProperty]
        private ObservableCollection<UsableModel> allUsables;

        [ObservableProperty]
        private ObservableCollection<ClothingModel> allClothing;

        [ObservableProperty]
        private ObservableCollection<ConsumableModel> allConsumables;

        #endregion

        #region Constructor

        public ItemCardViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore)
        {
            Name = Regex.Replace(nameof(ItemCardViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Create, view and edit Item Cards";
            Type = ViewModelType.ItemCardCreator;

            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;

            SetUnsetDatabaseEvents(true);

            LoadData();

            AllArmoursCollectionView = CollectionViewSource.GetDefaultView(AllArmours);
            AllWeaponsCollectionView = CollectionViewSource.GetDefaultView(AllWeapons);
            AllUsablesCollectionView = CollectionViewSource.GetDefaultView(AllUsables);
            AllClothingCollectionView = CollectionViewSource.GetDefaultView(AllClothing);
            AllConsumablesCollectionView = CollectionViewSource.GetDefaultView(AllConsumables);

            SetSelectionFromNavigation();
        }

        #endregion

        #region Private methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllItemCards = new(_cardDesignerStore.ItemCards);
            AllArmours = new(_cardDesignerStore.Armours);
            AllWeapons = new(_cardDesignerStore.Weapons);
            AllUsables = new(_cardDesignerStore.Usables);
            AllClothing = new(_cardDesignerStore.Clothings);
            AllConsumables = new(_cardDesignerStore.Consumables);

            SelectedItemCard = AllItemCards.FirstOrDefault();
        }

        private void SetUnsetDatabaseEvents(bool set)
        {
            if (set)
            {
                _cardDesignerStore.ItemCardChanged += OnItemCardChanged;
                _navigationStore.CurrentViewModelChanged += OnNavigatingAway;
            }
            else
            {
                _cardDesignerStore.ItemCardChanged -= OnItemCardChanged;
                _navigationStore.CurrentViewModelChanged -= OnNavigatingAway;
            }
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

        private bool UsablesFilter(object obj)
        {
            //your logicComplexFilter
            UsableModel usables = (UsableModel)obj;
            return
                WeaponSearchFilter == null ? false :
               usables.Name.Contains(UsableSearchFilter, StringComparison.OrdinalIgnoreCase)
            || usables.Uses.ToString().Contains(UsableSearchFilter, StringComparison.OrdinalIgnoreCase)
            || usables.UseTimeType.ToString().Contains(UsableSearchFilter, StringComparison.OrdinalIgnoreCase)
            || usables.UseTimeValue.ToString().Contains(UsableSearchFilter, StringComparison.OrdinalIgnoreCase);
        }


        private bool ClothingFilter(object obj)
        {
            //your logicComplexFilter
            ClothingModel clothings = (ClothingModel)obj;
            return
                ClothingSearchFilter == null ? false :
               clothings.Name.Contains(ClothingSearchFilter, StringComparison.OrdinalIgnoreCase)
            || clothings.ArmourType.ToString().Contains(ClothingSearchFilter, StringComparison.OrdinalIgnoreCase)
            || clothings.EquipmentSlot.ToString().Contains(ClothingSearchFilter, StringComparison.OrdinalIgnoreCase);
        }

        private bool ConsumablesFilter(object obj)
        {
            //your logicComplexFilter
            ConsumableModel consumables = (ConsumableModel)obj;
            return
                ConsumableSearchFilter == null ? false :
               consumables.Name.Contains(ConsumableSearchFilter, StringComparison.OrdinalIgnoreCase)
            || consumables.Doses.ToString().Contains(ConsumableSearchFilter, StringComparison.OrdinalIgnoreCase)
            || consumables.UseTimeType.ToString().Contains(ConsumableSearchFilter, StringComparison.OrdinalIgnoreCase)
            || consumables.UseTimeValue.ToString().Contains(ConsumableSearchFilter, StringComparison.OrdinalIgnoreCase);
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

        #region Database update methods

        private void OnItemCardChanged(ItemCardModel itemCard, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllItemCards.Add(itemCard);
                    SelectedItemCard = itemCard;
                    break;
                case DataChangeType.Deleted:
                    AllItemCards.Remove(itemCard);
                    SelectedItemCard = AllItemCards.FirstOrDefault();
                    break;
                case DataChangeType.Updated:
                    SelectedItemCard = itemCard;
                    break;
                default:
                    break;
            }
        }

        partial void OnSelectedItemCardChanged(ItemCardModel value)
        {
            if (value != null)
            {
                switch (value.Type)
                {
                    case ItemType.Weapon:
                        SelectedItemCard.Item = AllWeapons.FirstOrDefault(i => i.ID == value.ItemID);
                        break;
                    case ItemType.Armour:
                        SelectedItemCard.Item = AllArmours.FirstOrDefault(i => i.ID == value.ItemID);
                        break;
                    case ItemType.Usable:
                        SelectedItemCard.Item = AllUsables.FirstOrDefault(i => i.ID == value.ItemID);
                        break;
                    case ItemType.Clothing:
                        SelectedItemCard.Item = AllClothing.FirstOrDefault(i => i.ID == value.ItemID);
                        break;
                    case ItemType.Consumable:
                        SelectedItemCard.Item = AllConsumables.FirstOrDefault(i => i.ID == value.ItemID);
                        break;
                    default:
                        break;
                }
            }
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
                                SelectedItemCard = _navigationStore.SelectedItemCard;
                                SelectedItemDeckDesign = _navigationStore.SelectedItemDeckDesign;
                                return;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        SelectedItemCard = AllItemCards.FirstOrDefault();
                        SelectedItemDeckDesign = new();
                    }
            }
        }

        private void OnNavigatingAway(ViewModelType type)
        {
            SetUnsetDatabaseEvents(false);
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

        [RelayCommand]
        private async void DeleteItemCard()
        {
            await _cardDesignerStore.DeleteItemCard(SelectedItemCard);
        }

        #endregion
    }
}