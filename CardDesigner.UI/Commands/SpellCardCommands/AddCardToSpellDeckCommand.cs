using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System.ComponentModel;

namespace CardDesigner.UI.Commands
{
    public class AddCardToSpellDeckCommand : CommandBase
    {
        private readonly SpellDeckViewModel _SpellDeckViewModel;
        private readonly CardDesignerStore _cardDesignerStore;

        public AddCardToSpellDeckCommand(SpellDeckViewModel SpellDeckViewModel, CardDesignerStore cardDesignerStore)
        {
            _SpellDeckViewModel = SpellDeckViewModel;
            _cardDesignerStore = cardDesignerStore;
            _SpellDeckViewModel.PropertyChanged += PropertyChangedEventHandle;
        }

        private void PropertyChangedEventHandle(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SpellDeckViewModel.SelectedSpellDeck) || e.PropertyName == nameof(SpellDeckViewModel.SelectedSpellCard))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _SpellDeckViewModel.SelectedSpellDeck != null
                && _SpellDeckViewModel.SelectedSpellCard != null;
        }

        public override void Execute(object parameter)
        {
            _SpellDeckViewModel.SelectedSpellDeck.SpellCards.Add(_SpellDeckViewModel.SelectedSpellCard);
            _cardDesignerStore.UpdateSpellDeck(_SpellDeckViewModel.SelectedSpellDeck);
        }
    }
}
