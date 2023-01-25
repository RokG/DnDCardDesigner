using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
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

        #endregion

        #region Properties

        [ObservableProperty]
        private MagicSchool magicSchoolType;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateSpellCardCommand))]
        private string spellCardName;

        [ObservableProperty]
        private SpellCardModel selectedSpellCard;

        [ObservableProperty]
        private ObservableCollection<SpellCardModel> allSpellCards;

        #endregion Properties

        #region Constructor

        public SpellCardViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = Regex.Replace(nameof(SpellCardViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Create, view and edit Spell Cards";

            _cardDesignerStore = cardDesignerStore;

            _cardDesignerStore.SpellCardCreated += OnSpellCardCreated;

            // TODO: is this OK? how is it different from old method (before MVVM toolkit)
            LoadData();
        }

        #endregion

        #region Private methods

        private async void LoadData()
        {
            await _cardDesignerStore.Load();

            AllSpellCards = new(_cardDesignerStore.SpellCards);

            SelectedSpellCard = AllSpellCards.FirstOrDefault();
        }

        private void OnSpellCardCreated(SpellCardModel spellCard)
        {
            AllSpellCards.Add(spellCard);
            SelectedSpellCard = spellCard;
        }

        #endregion

        #region Public methods

        public static SpellCardViewModel LoadViewModel(CardDesignerStore cardDesignerStore)
        {
            SpellCardViewModel viewModel = new(cardDesignerStore);
            viewModel.LoadData();

            return viewModel;
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

        #endregion
    }
}