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
        private readonly CharacterViewModel _characterViewModel;
        private readonly CardDesignerStore _cardDesignerStore;

        public AddDeckToCharacterCommand(CharacterViewModel characterViewModel, CardDesignerStore cardDesignerStore)
        {
            _characterViewModel = characterViewModel;
            _cardDesignerStore = cardDesignerStore;
            _characterViewModel.PropertyChanged += PropertyChangedEventHandle;
        }

        private void PropertyChangedEventHandle(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CharacterViewModel.SelectedCharacter) || e.PropertyName == nameof(CharacterViewModel.SelectedSpellDeck))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _characterViewModel.SelectedCharacter != null
                && _characterViewModel.SelectedSpellDeck != null;
        }

        public override void Execute(object parameter)
        {
            _characterViewModel.SelectedCharacter.SpellDeck = _characterViewModel.SelectedSpellDeck;
            _cardDesignerStore.UpdateCharacter(_characterViewModel.SelectedCharacter);
        }
    }
}
