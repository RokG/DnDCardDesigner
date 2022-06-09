using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System.ComponentModel;
using System.Linq;

namespace CardDesigner.UI.Commands
{
    public class CreateSpellDeckCommand : CommandBase
    {
        private readonly CharacterViewModel _characterViewModel;
        private readonly CardDesignerStore _cardDesignerStore;

        public CreateSpellDeckCommand(CharacterViewModel characterViewModel, CardDesignerStore cardDesignerStore)
        {
            _characterViewModel = characterViewModel;
            _cardDesignerStore = cardDesignerStore;
            _characterViewModel.PropertyChanged += PropertyChangedEventHandle;
        }

        private void PropertyChangedEventHandle(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CharacterViewModel.AddedSpellDeckName))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _characterViewModel.AddedSpellDeckName != string.Empty
                && !_characterViewModel.AllSpellDecks.Where(c => c.Name == _characterViewModel.AddedSpellDeckName).Any();
        }

        public override void Execute(object parameter)
        {
            _cardDesignerStore.CreateSpellDeck(new SpellDeckModel() { Name = _characterViewModel.AddedSpellDeckName });
        }
    }
}