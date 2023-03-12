using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;

namespace CardDesigner.UI.ViewModels
{
    public partial class MinionViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;
        private readonly NavigationStore _navigationStore;
        private readonly SettingsStore _settingsStore;

        #endregion

        #region Properties

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateMinionCommand))]
        private string addedMinionName;

        [ObservableProperty]
        private MinionModel selectedMinion;

        [ObservableProperty]
        private ObservableCollection<MinionModel> allMinions;

        #endregion

        #region Constructor

        public MinionViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore, SettingsStore settingsStore)
        {
            Name = ModuleNameRegex().Replace(nameof(MinionViewModel).Replace("ViewModel", ""), " $1");
            Description = "Create, view and edit Minions";
            Type = ViewModelType.MinionCreator;

            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;
            _settingsStore = settingsStore;

            SetUnsetDatabaseEvents(true);

            LoadData();

            SelectedMinion = AllMinions.FirstOrDefault();
        }

        #endregion

        #region Private methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllMinions = new(_cardDesignerStore.Minions);
        }

        private void SetUnsetDatabaseEvents(bool set)
        {
            if (set)
            {
                _cardDesignerStore.MinionChanged += OnMinionChanged;
                _navigationStore.CurrentViewModelChanged += OnNavigatingAway;
            }
            else
            {
                _cardDesignerStore.MinionChanged -= OnMinionChanged;
                _navigationStore.CurrentViewModelChanged -= OnNavigatingAway;
            }
        }

        #endregion

        #region Public methods

        public static MinionViewModel LoadViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore, SettingsStore settingsStore)
        {
            MinionViewModel viewModel = new(cardDesignerStore, navigationStore, settingsStore);

            viewModel.LoadData();

            return viewModel;
        }

        #endregion

        #region Database update methods

        private void OnMinionChanged(MinionModel minion, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllMinions.Add(minion);
                    SelectedMinion = minion;
                    break;
                case DataChangeType.Updated:
                    SelectedMinion = minion;
                    break;
                case DataChangeType.Deleted:
                    AllMinions.Remove(SelectedMinion);
                    SelectedMinion = AllMinions.FirstOrDefault();
                    break;
                default:
                    break;
            }
        }

        #endregion  

        #region Navigation

        private void OnNavigatingAway(ViewModelType type)
        {
            SetUnsetDatabaseEvents(false);
        }

        #endregion

        #region Commands

        [RelayCommand(CanExecute = nameof(CanCreateMinion))]
        private async void CreateMinion()
        {
            await _cardDesignerStore.CreateMinion(new MinionModel() { Name = AddedMinionName });
        }

        private bool CanCreateMinion()
        {
            bool noName = (AddedMinionName == string.Empty || AddedMinionName == null);
            bool spellDeckExists = AllMinions != null && AllMinions.Where(c => c.Name == AddedMinionName).Any();

            return (!noName && !spellDeckExists);
        }

        [RelayCommand]
        private async void DeleteMinion()
        {
            await _cardDesignerStore.DeleteMinion(SelectedMinion);
        }

        [RelayCommand]
        private async void UpdateMinion()
        {
            await _cardDesignerStore.UpdateMinion(SelectedMinion);
        }

        #endregion
    }
}
