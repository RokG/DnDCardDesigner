using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace CardDesigner.UI.Commands
{
    public class CreateCharacterCommand : CommandBase
    {
        private readonly CharacterViewModel _characterViewModel;
        private readonly CardDesignerStore _cardDesignerStore;

        public CreateCharacterCommand(CharacterViewModel viewModel, CardDesignerStore cardDesignerStore)
        {
            _characterViewModel = viewModel;
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
                && !_characterViewModel.AllCharacters.Where(c=>c.Name == _characterViewModel.AddedCharacterName).Any();
        }

        public override void Execute(object parameter)
        {
            Debug.WriteLine("Added Character");
            _cardDesignerStore.CreateCharacter(new CharacterModel() { Name = _characterViewModel.AddedCharacterName });

            //_cardDesignerStore.Load();

            //_characterViewModel.AllCharacters = new(_cardDesignerStore.Characters);
            //_characterViewModel.AllSpellCards = new(_cardDesignerStore.SpellCards);
            //_characterViewModel.AllSpellDecks = new(_cardDesignerStore.SpellDecks);

        }
    }
}