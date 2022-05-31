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
            if (e.PropertyName == nameof(CharacterViewModel.AddedItemName))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _characterViewModel.AddedItemName != string.Empty
                && !_characterViewModel.AllCharacters.Where(c=>c.Name == _characterViewModel.AddedItemName).Any();
        }

        public override void Execute(object parameter)
        {
            Debug.WriteLine("Added Character");
            _cardDesignerStore.CreateCharacter(new CharacterModel() { Name = _characterViewModel.AddedItemName });
        }
    }
}