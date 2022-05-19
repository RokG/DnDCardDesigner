using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
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
        private readonly Character _character;

        public AddCardCommand(CardCreatorViewModel cardCreatorViewModel, Character character)
        {
            _cardCreatorViewModel = cardCreatorViewModel;
            _character = character;
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
            _character.AddCardToDeck(new SpellCard() { Name = "asdadad" }, _cardCreatorViewModel.DesiredType);
        }
    }
}