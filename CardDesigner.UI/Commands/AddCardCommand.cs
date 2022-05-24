using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System.ComponentModel;
using System.Diagnostics;

namespace CardDesigner.UI.Commands
{
    public class AddCardCommand : CommandBase
    {
        private readonly CardCreatorViewModel _cardCreatorViewModel;
        private readonly CardDesignerStore _cardDesignerStore;

        public AddCardCommand(CardCreatorViewModel cardCreatorViewModel, CardDesignerStore cardDesignerStore)
        {
            _cardCreatorViewModel = cardCreatorViewModel;
            _cardDesignerStore = cardDesignerStore;
            _cardCreatorViewModel.PropertyChanged += PropertyChangedEventHandle;
        }

        private void PropertyChangedEventHandle(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CardCreatorViewModel.MagicSchoolType))
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
            _cardDesignerStore.AddCardToCharacter(_cardCreatorViewModel.SelectedCharacter, _cardCreatorViewModel.SelectedCard);
        }
    }
}