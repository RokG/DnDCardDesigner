using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.Commands;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public class CardCreatorViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CharacterModel _character;

        #endregion

        #region Properties

        private MagicSchool _magicSchoolType;
        public MagicSchool MagicSchoolType
        {
            get => _magicSchoolType;
            set => SetProperty(ref _magicSchoolType, value);
        }

        private ICard _selectedCard;
        public ICard SelectedCard
        {
            get => _selectedCard;
            set => SetProperty(ref _selectedCard, value);
        }
        public CharacterModel SelectedCharacter { get; set; }

        #endregion Properties

        #region Actions, Events, Commands

        public ICommand AddCardCommand { get; }
        public ICommand DoNavigateCommand { get; }

        #endregion Actions, Events, Commands

        #region Constructor

        public CardCreatorViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = nameof(CardCreatorViewModel).Replace("ViewModel", "");

            SelectedCharacter = new CharacterModel("Genlamin") {ID = 1 };

            AddCardCommand = new AddCardCommand(this, cardDesignerStore);
            //DoNavigateCommand = new NavigateCommand(navigationService);

            SelectedCard = new SpellCardModel() { Name = "blabla", ID = 1};
        }

        #endregion

        #region Private methods

        #endregion

        #region Public methods

        public static CardCreatorViewModel LoadViewModel(CardDesignerStore cardDesignerStore)
        {
            return new(cardDesignerStore);
        }

        #endregion

    }
}