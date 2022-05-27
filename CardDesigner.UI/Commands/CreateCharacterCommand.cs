using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System.ComponentModel;

namespace CardDesigner.UI.Commands
{
    public class CreateCharacterCommand : CommandBase
    {
        private readonly SpellCardViewModel _cardCreatorViewModel;
        private readonly CardDesignerStore _cardDesignerStore;

        public CreateCharacterCommand(SpellCardViewModel cardCreatorViewModel, CardDesignerStore cardDesignerStore)
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
            return _cardCreatorViewModel.MagicSchoolType == MagicSchool.None && base.CanExecute(parameter);
            //return true;
        }

        public override void Execute(object parameter)
        {
            _cardDesignerStore.CreateCharacter(_cardCreatorViewModel.SelectedCharacter);
        }
    }
}