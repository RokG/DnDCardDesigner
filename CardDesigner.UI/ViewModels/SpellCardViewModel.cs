using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace CardDesigner.UI.ViewModels
{
    public partial class SpellCardViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;
        private readonly NavigationStore _navigationStore;
        private readonly SettingsStore _settingsStore;

        #endregion

        #region Properties

        [ObservableProperty]
        private MagicSchool magicSchoolType;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateSpellCardCommand))]
        private string spellCardName;

        [ObservableProperty]
        private SpellDeckDesignModel selectedSpellDeckDesign = new();

        [ObservableProperty]
        private SpellCardModel selectedSpellCard;

        [ObservableProperty]
        private ObservableCollection<SpellCardModel> allSpellCards;

        #endregion Properties

        #region Constructor

        public SpellCardViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore, SettingsStore settingsStore)
        {
            Name = Regex.Replace(nameof(SpellCardViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Create, view and edit Spell Cards";
            Type = ViewModelType.SpellCardCreator;

            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;
            _settingsStore = settingsStore;

            SetUnsetDatabaseEvents(true);

            LoadData();

            SetSelectionFromNavigation();
        }

        #endregion

        #region Private methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllSpellCards = new(_cardDesignerStore.SpellCards);

            SelectedSpellCard = AllSpellCards.FirstOrDefault();
        }

        private void SetUnsetDatabaseEvents(bool set)
        {
            if (set)
            {
                _cardDesignerStore.SpellCardChanged += OnSpellCardChanged;
                _navigationStore.CurrentViewModelChanged += OnNavigatingAway;
            }
            else
            {
                _cardDesignerStore.SpellCardChanged -= OnSpellCardChanged;
                _navigationStore.CurrentViewModelChanged -= OnNavigatingAway;
            }
        }

        #endregion

        #region Public methods

        public static SpellCardViewModel LoadViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore, SettingsStore settingsStore)
        {
            SpellCardViewModel viewModel = new(cardDesignerStore, navigationStore, settingsStore);
            viewModel.LoadData();

            return viewModel;
        }

        #endregion

        #region Database update methods

        private void OnSpellCardChanged(SpellCardModel spellCard, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllSpellCards.Add(spellCard);
                    SelectedSpellCard = spellCard;
                    break;
                case DataChangeType.Deleted:
                    AllSpellCards.Remove(spellCard);
                    SelectedSpellCard = AllSpellCards.FirstOrDefault();
                    break;
                default:
                    break;
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
                            SelectedSpellCard = AllSpellCards.FirstOrDefault(ic => ic.ID == _navigationStore.SelectedSpellCard.ID);
                            SelectedSpellDeckDesign = _navigationStore.SelectedSpellDeckDesign;
                            return;
                        default:
                            break;
                    }
                }
                else
                {
                    SelectedSpellCard = AllSpellCards.FirstOrDefault();
                    SelectedSpellDeckDesign = new();
                }
            }
        }

        private void OnNavigatingAway(ViewModelType type)
        {
            SetUnsetDatabaseEvents(false);
        }

        #endregion

        #region Commands

        [RelayCommand(CanExecute = nameof(CanCreateSpellCard))]
        private async void CreateSpellCard()
        {
            await _cardDesignerStore.CreateSpellCard(new SpellCardModel() { Name = SpellCardName });
        }

        private bool CanCreateSpellCard()
        {
            return SpellCardName != null
                && SpellCardName != string.Empty
                && !AllSpellCards.Where(c => c.Name == SpellCardName).Any();
        }

        [RelayCommand]
        private async void UpdateSpellCard()
        {
            await _cardDesignerStore.UpdateSpellCard(SelectedSpellCard);
        }

        [RelayCommand]
        private async void DeleteSpellCard()
        {
            await _cardDesignerStore.DeleteSpellCard(SelectedSpellCard);
        }

        #endregion
    }
}