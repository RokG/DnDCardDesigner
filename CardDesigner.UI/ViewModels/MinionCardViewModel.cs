using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CardDesigner.UI.ViewModels
{
    public partial class MinionCardViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;
        private readonly NavigationStore _navigationStore;
        private readonly SettingsStore _settingsStore;

        #endregion

        #region Properties

        [ObservableProperty]
        private MinionModel selectedMinion;

        [ObservableProperty]
        private ObservableCollection<MinionModel> allMinions;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateMinionCardCommand))]
        private string minionCardName;

        [ObservableProperty]
        private MinionDeckDesignModel selectedMinionDeckDesign = new();

        [ObservableProperty]
        private MinionCardModel selectedMinionCard;

        [ObservableProperty]
        private ObservableCollection<MinionCardModel> allMinionCards;

        #endregion

        #region Constructor

        public MinionCardViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore, SettingsStore settingsStore)
        {
            Name = ModuleNameRegex().Replace(nameof(MinionCardViewModel).Replace("ViewModel", ""), " $1");
            Description = "Create, view and edit Minions";
            Type = ViewModelType.MinionCardCreator;

            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;
            _settingsStore = settingsStore;

            SetUnsetDatabaseEvents(true);

            LoadData();
        }

        #endregion

        #region Private methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllMinions = new(_cardDesignerStore.Minions);
            AllMinionCards = new(_cardDesignerStore.MinionCards);
            //SelectedMinion = SelectedMinionCard.Minion ?? AllMinions.FirstOrDefault();
            SelectedMinion = AllMinions.FirstOrDefault(m => m.ID == SelectedMinionCard?.Minion?.ID);
            SelectedMinionCard = AllMinionCards.FirstOrDefault();
        }

        private void SetUnsetDatabaseEvents(bool set)
        {
            if (set)
            {
                _cardDesignerStore.MinionCardChanged += OnMinionCardChanged;
                _navigationStore.CurrentViewModelChanged += OnNavigatingAway;
            }
            else
            {
                _cardDesignerStore.MinionCardChanged -= OnMinionCardChanged;
                _navigationStore.CurrentViewModelChanged -= OnNavigatingAway;
            }
        }

        #endregion

        #region Public methods

        public static MinionCardViewModel LoadViewModel(CardDesignerStore cardDesignerStore, NavigationStore navigationStore, SettingsStore settingsStore)
        {
            MinionCardViewModel viewModel = new(cardDesignerStore, navigationStore, settingsStore);

            viewModel.LoadData();

            return viewModel;
        }

        partial void OnSelectedMinionCardChanged(MinionCardModel value)
        {
            SelectedMinion = AllMinions.FirstOrDefault(m => m.ID == value?.Minion?.ID);
        }

        partial void OnSelectedMinionChanged(MinionModel value)
        {
            if (value != null)
            {
                SelectedMinionCard.Minion = SelectedMinion;
                // TODO: Had to do it like this to prevent warnings on unawaited calls
                Task.Run(async () => { await _cardDesignerStore.UpdateMinionCard(SelectedMinionCard); });
            }
        }

        #endregion

        #region Database update methods

        private void OnMinionCardChanged(MinionCardModel minionCard, DataChangeType change)
        {
            switch (change)
            {
                case DataChangeType.Created:
                    AllMinionCards.Add(minionCard);
                    SelectedMinionCard = minionCard;
                    break;
                case DataChangeType.Updated:
                    AllMinionCards = new(_cardDesignerStore.MinionCards);
                    SelectedMinionCard = AllMinionCards.FirstOrDefault(mc => mc.ID == minionCard.ID);
                    break;
                case DataChangeType.Deleted:
                    AllMinionCards.Remove(SelectedMinionCard);
                    SelectedMinionCard = AllMinionCards.FirstOrDefault();
                    SelectedMinion = AllMinions.FirstOrDefault(m => m.ID == minionCard?.Minion?.ID);
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

        [RelayCommand(CanExecute = nameof(CanCreateMinionCard))]
        private async void CreateMinionCard()
        {
            await _cardDesignerStore.CreateMinionCard(new MinionCardModel() { Name = MinionCardName });
        }

        private bool CanCreateMinionCard()
        {
            return MinionCardName != null
                && MinionCardName != string.Empty
                && !AllMinionCards.Where(c => c.Name == MinionCardName).Any();
        }

        [RelayCommand]
        private async void UpdateMinionCard()
        {
            SelectedMinionCard.Minion = SelectedMinion;
            await _cardDesignerStore.UpdateMinionCard(SelectedMinionCard);
        }

        [RelayCommand]
        private async void DeleteMinionCard()
        {
            await _cardDesignerStore.DeleteMinionCard(SelectedMinionCard);
        }

        #endregion
    }
}
