using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using CardDesigner.UI.Commands;
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

        public CardDisplayViewModel(CharacterModel character, NavigationService navigationService)
        {
            Name = nameof(CardDisplayViewModel).Replace("ViewModel", "");

            _character = character;

            DoNavigateCommand = new NavigateCommand(navigationService);

            UpdateCardView();
        }

        private void UpdateCardView()
        {
            var a = _character.GetCharacterSpellDeck();
            CardName = a.FirstOrDefault().Name;
        }
    }
}