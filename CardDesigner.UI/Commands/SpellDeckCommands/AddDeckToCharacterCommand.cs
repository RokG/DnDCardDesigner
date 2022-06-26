using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace CardDesigner.UI.Commands
{
    public class AddDeckToCharacterCommand : CommandBase
    {
        private readonly SpellDeckViewModel _SpellDeckViewModel;
        private readonly CardDesignerStore _cardDesignerStore;

        public AddDeckToCharacterCommand(SpellDeckViewModel SpellDeckViewModel, CardDesignerStore cardDesignerStore)
        {
            _SpellDeckViewModel = SpellDeckViewModel;
            _cardDesignerStore = cardDesignerStore;
            _SpellDeckViewModel.PropertyChanged += PropertyChangedEventHandle;
        }

        private void PropertyChangedEventHandle(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SpellDeckViewModel.SelectedCharacter) || e.PropertyName == nameof(SpellDeckViewModel.SelectedSpellDeck))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _SpellDeckViewModel.SelectedCharacter != null
                && _SpellDeckViewModel.SelectedSpellDeck != null;
        }

        public override void Execute(object parameter)
        {
            _SpellDeckViewModel.SelectedCharacter.SpellDeck = _SpellDeckViewModel.SelectedSpellDeck;
            _cardDesignerStore.UpdateCharacter(_SpellDeckViewModel.SelectedCharacter);
        }
    }
}
