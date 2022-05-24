using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using System.Linq;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public class CardDisplayViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CharacterModel _character;

        #endregion

        #region Properties

        private string _cardName;
        public string CardName
        {
            get => _cardName;
            set => SetProperty(ref _cardName, value);
        }

        #endregion

        #region Actions, Events, Commands

        public ICommand DoNavigateCommand { get; }

        #endregion Actions, Events, Commands

        #region Constructor

        public CardDisplayViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = nameof(CardDisplayViewModel).Replace("ViewModel", "");
        }

        #endregion

        #region Private methods

        private void UpdateCardView()
        {
            System.Collections.Generic.List<SpellCardModel> a = _character.GetCharacterSpellDeck();
            CardName = a.FirstOrDefault().Name;
        }

        #endregion

        #region Public methods

        public static CardDisplayViewModel LoadViewModel(CardDesignerStore cardDesignerStore)
        {
            return new(cardDesignerStore);
        }

        #endregion
    }
}