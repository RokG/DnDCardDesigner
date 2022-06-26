using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;

namespace CardDesigner.UI.Commands
{
    public class CreateSpellCardCommand : CommandBase
    {
        private readonly SpellCardViewModel _SpellCardViewModel;
        private readonly CardDesignerStore _cardDesignerStore;

        public CreateSpellCardCommand(SpellCardViewModel SpellCardViewModel, CardDesignerStore cardDesignerStore)
        {
            _SpellCardViewModel = SpellCardViewModel;
            _cardDesignerStore = cardDesignerStore;
            _SpellCardViewModel.PropertyChanged += PropertyChangedEventHandle;
        }

        private void PropertyChangedEventHandle(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SpellCardViewModel.SelectedSpellDeck))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _SpellCardViewModel.SelectedSpellDeck != null;
        }

        public override void Execute(object parameter)
        {
            _cardDesignerStore.CreateSpellCard(new SpellCardModel() { Name = RandomString(7) });
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}