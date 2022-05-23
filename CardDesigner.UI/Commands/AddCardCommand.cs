using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using CardDesigner.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesigner.UI.Commands
{
    public class AddCardCommand : CommandBase
    {
        private readonly CardCreatorViewModel _cardCreatorViewModel;
        private readonly CharacterModel _character;
        private readonly NavigationService _navigationService;

        public AddCardCommand(CardCreatorViewModel cardCreatorViewModel, CharacterModel character, NavigationService navigationService)
        {
            _cardCreatorViewModel = cardCreatorViewModel;
            _character = character;
            _navigationService = navigationService;
            _cardCreatorViewModel.PropertyChanged += PropertyChangedEventHandle;
        }

        private void PropertyChangedEventHandle(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CardCreatorViewModel.DesiredType))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _cardCreatorViewModel.DesiredType == DeckType.Spells && base.CanExecute(parameter);
            //return true;
        }

        public override void Execute(object? parameter)
        {
            _character.AddSpellCardToDeck(new SpellCardModel() { Name = "asdadad" });

            _navigationService.Navigate();
        }
    }
}