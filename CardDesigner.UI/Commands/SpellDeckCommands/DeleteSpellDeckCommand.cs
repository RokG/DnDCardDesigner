using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System.ComponentModel;

namespace CardDesigner.UI.Commands
{
    public class DeleteSpellDeckCommand : CommandBase
    {
        private readonly SpellDeckViewModel _SpellDeckViewModel;
        private readonly CardDesignerStore _cardDesignerStore;

        public DeleteSpellDeckCommand(SpellDeckViewModel SpellDeckViewModel, CardDesignerStore cardDesignerStore)
        {
            _SpellDeckViewModel = SpellDeckViewModel;
            _cardDesignerStore = cardDesignerStore;
            _SpellDeckViewModel.PropertyChanged += PropertyChangedEventHandle;
        }

        private void PropertyChangedEventHandle(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SpellDeckViewModel.SelectedSpellDeck))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _SpellDeckViewModel.SelectedSpellDeck != null;
        }

        public override void Execute(object parameter)
        {
            //if (_cardDesignerStore.SpellDecks.Single(c => c.ID == _SpellDeckViewModel.SelectedSpellDeck.ID) is SpellDeckModel spellDeckToDelete)
            //{
            //    _cardDesignerStore.DeleteSpellDeck(spellDeckToDelete);
            //}
            _cardDesignerStore.DeleteSpellDeck(_SpellDeckViewModel.SelectedSpellDeck);
        }
    }
}