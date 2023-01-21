using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System.ComponentModel;
using System.Linq;

namespace CardDesigner.UI.Commands
{
    public class CreateSpellDeckCommand : CommandBase
    {
        private readonly SpellDeckViewModel _SpellDeckViewModel;
        private readonly CardDesignerStore _cardDesignerStore;

        public CreateSpellDeckCommand(SpellDeckViewModel SpellDeckViewModel, CardDesignerStore cardDesignerStore)
        {
            _SpellDeckViewModel = SpellDeckViewModel;
            _cardDesignerStore = cardDesignerStore;
            _SpellDeckViewModel.PropertyChanged += PropertyChangedEventHandle;
        }

        private void PropertyChangedEventHandle(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SpellDeckViewModel.AddedSpellDeckName))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            bool noName = (_SpellDeckViewModel.AddedSpellDeckName == string.Empty || _SpellDeckViewModel.AddedSpellDeckName == null);
            bool spellDeckExists = _SpellDeckViewModel.AllSpellDecks == null ? false : _SpellDeckViewModel.AllSpellDecks.Where(c => c.Name == _SpellDeckViewModel.AddedSpellDeckName).Any();

            return (!noName && !spellDeckExists);
        }

        public override void Execute(object parameter)
        {
            _cardDesignerStore.CreateSpellDeck(new SpellDeckModel() { Name = _SpellDeckViewModel.AddedSpellDeckName });
        }
    }
}