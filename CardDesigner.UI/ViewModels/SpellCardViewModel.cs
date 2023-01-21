using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Input;

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
        private string spellCardName;

        [ObservableProperty]
        private SpellCardModel selectedSpellCard;

        [ObservableProperty]
        private ObservableCollection<SpellCardModel> allSpellCards;

        #endregion Properties

        #region Actions, Events, Commands

        public ICommand CreateSpellCardCommand { get; }
        public ICommand UpdateSpellCardCommand { get; }

        #endregion Actions, Events, Commands

        #region Constructor

        public SpellCardViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = Regex.Replace(nameof(SpellCardViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
            Description = "Create, view and edit Spell Cards";

            _cardDesignerStore = cardDesignerStore;

            CreateSpellCardCommand = new CreateSpellCardCommand(this, cardDesignerStore);
            UpdateSpellCardCommand = new UpdateSpellCardCommand(this, cardDesignerStore);

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

    }
}