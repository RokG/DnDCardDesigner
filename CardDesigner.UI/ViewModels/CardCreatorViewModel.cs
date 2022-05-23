using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public class CardCreatorViewModel : ViewModelBase
    {
        #region Fields

        private readonly CharacterModel _character;

        #endregion Fields

        #region Properties

        private MagicSchool _magicSchoolType;

        public MagicSchool MagicSchoolType
        {
            get => _magicSchoolType;
            set => SetProperty(ref _magicSchoolType, value);
        }

        private ICard? _selectedCard;

        public ICard? SelectedCard
        {
            get => _selectedCard;
            set => SetProperty(ref _selectedCard, value);
        }

        #endregion Properties

        #region Actions, Events, Commands

        public ICommand AddCardCommand { get; }
        public ICommand DoNavigateCommand { get; }

        #endregion Actions, Events, Commands

        public CardCreatorViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = nameof(CardCreatorViewModel).Replace("ViewModel", "");

            //AddCardCommand = new AddCardCommand(this, character, navigationService);
            //DoNavigateCommand = new NavigateCommand(navigationService);

            SelectedCard = new SpellCardModel() { Name = "blabla" };
        }

        public static CardCreatorViewModel LoadViewModel(CardDesignerStore cardDesignerStore)
        {
            return new(cardDesignerStore);
        }
    }
}