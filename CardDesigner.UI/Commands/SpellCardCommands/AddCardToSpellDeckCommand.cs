using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace CardDesigner.UI.Commands
{
    public class AddCardToSpellDeckCommand : CommandBase
    {
        private readonly CharacterViewModel _characterViewModel;
        private readonly CardDesignerStore _cardDesignerStore;

        public AddCardToSpellDeckCommand(CharacterViewModel characterViewModel, CardDesignerStore cardDesignerStore)
        {
            _characterViewModel = characterViewModel;
            _cardDesignerStore = cardDesignerStore;
            _characterViewModel.PropertyChanged += PropertyChangedEventHandle;
        }

        private void PropertyChangedEventHandle(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CharacterViewModel.SelectedSpellDeck) || e.PropertyName == nameof(CharacterViewModel.SelectedSpellCard))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _characterViewModel.SelectedSpellDeck != null
                && _characterViewModel.SelectedSpellCard != null;
        }

        public override void Execute(object parameter)
        {
            _characterViewModel.SelectedSpellDeck.SpellCards.Add(_characterViewModel.SelectedSpellCard);
            _cardDesignerStore.UpdateSpellDeck(_characterViewModel.SelectedSpellDeck);
        }
    }
}
