using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using System.Linq;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public class CardDisplayViewModel : ViewModelBase
    {
        private readonly CharacterModel _character;

        private string _cardName;

        public string CardName
        {
            get => _cardName;
            set => SetProperty(ref _cardName, value);
        }

        #region Actions, Events, Commands

        public ICommand DoNavigateCommand { get; }

        #endregion Actions, Events, Commands

        public CardDisplayViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = nameof(CardDisplayViewModel).Replace("ViewModel", "");
        }

        private void UpdateCardView()
        {
            var a = _character.GetCharacterSpellDeck();
            CardName = a.FirstOrDefault().Name;
        }

        public static CardDisplayViewModel LoadViewModel(CardDesignerStore cardDesignerStore)
        {
            return new(cardDesignerStore);
        }
    }
}