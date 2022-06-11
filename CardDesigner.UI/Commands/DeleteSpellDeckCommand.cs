using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System.ComponentModel;
using System.Linq;

namespace CardDesigner.UI.Commands
{
    public class DeleteSpellDeckCommand : CommandBase
    {
        private readonly CharacterViewModel _characterViewModel;
        private readonly CardDesignerStore _cardDesignerStore;

        public DeleteSpellDeckCommand(CharacterViewModel characterViewModel, CardDesignerStore cardDesignerStore)
        {
            _characterViewModel = characterViewModel;
            _cardDesignerStore = cardDesignerStore;
            _characterViewModel.PropertyChanged += PropertyChangedEventHandle;
        }

        private void PropertyChangedEventHandle(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CharacterViewModel.AddedCharacterName))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _characterViewModel.AddedCharacterName != string.Empty
                && !_characterViewModel.AllCharacters.Where(c => c.Name == _characterViewModel.AddedCharacterName).Any();
        }

        public override void Execute(object parameter)
        {
            //if (_cardDesignerStore.SpellDecks.Single(c => c.ID == _characterViewModel.SelectedSpellDeck.ID) is SpellDeckModel spellDeckToDelete)
            //{
            //    _cardDesignerStore.DeleteSpellDeck(spellDeckToDelete);
            //}
            _cardDesignerStore.DeleteSpellDeck(_characterViewModel.SelectedSpellDeck);
        }
    }
}