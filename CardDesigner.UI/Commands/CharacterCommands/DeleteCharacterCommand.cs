using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System.ComponentModel;
using System.Linq;

namespace CardDesigner.UI.Commands
{
    public class DeleteCharacterCommand : CommandBase
    {
        private readonly SpellCardViewModel _SpellCardViewModel;
        private readonly CardDesignerStore _cardDesignerStore;

        public DeleteCharacterCommand(SpellCardViewModel SpellCardViewModel, CardDesignerStore cardDesignerStore)
        {
            _SpellCardViewModel = SpellCardViewModel;
            _cardDesignerStore = cardDesignerStore;
            _SpellCardViewModel.PropertyChanged += PropertyChangedEventHandle;
        }

        private void PropertyChangedEventHandle(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SpellCardViewModel.SelectedCharacter))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _SpellCardViewModel.SelectedCharacter != null;
        }

        public override void Execute(object parameter)
        {
            _cardDesignerStore.DeleteCharacter(_SpellCardViewModel.SelectedCharacter);
        }
    }
}