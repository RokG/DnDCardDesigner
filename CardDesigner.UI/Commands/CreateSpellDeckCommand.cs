using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;

namespace CardDesigner.UI.Commands
{
    public class CreateSpellDeckCommand : CommandBase
    {
        private readonly SpellCardViewModel _cardCreatorViewModel;
        private readonly CardDesignerStore _cardDesignerStore;

        public CreateSpellDeckCommand(SpellCardViewModel cardCreatorViewModel, CardDesignerStore cardDesignerStore)
        {
            _cardCreatorViewModel = cardCreatorViewModel;
            _cardDesignerStore = cardDesignerStore;
            _cardCreatorViewModel.PropertyChanged += PropertyChangedEventHandle;
        }

        private void PropertyChangedEventHandle(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SpellCardViewModel.MagicSchoolType))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _cardCreatorViewModel.MagicSchoolType == MagicSchool.Abjuration && base.CanExecute(parameter);
            //return true;
        }

        public override void Execute(object parameter)
        {
            _cardCreatorViewModel.AddRandomDeck();
            _cardCreatorViewModel.AddRandomCardToDeck();
            _cardCreatorViewModel.AddRandomCardToDeck();
            _cardCreatorViewModel.AddRandomCardToDeck();
            _cardDesignerStore.CreateSpellDeck(_cardCreatorViewModel.SelectedSpellDeck);
        }

        
    }
}