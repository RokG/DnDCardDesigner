using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;

namespace CardDesigner.UI.Commands
{
    public class UpdateSpellCardCommand : CommandBase
    {
        private readonly SpellCardViewModel _SpellCardViewModel;
        private readonly CardDesignerStore _cardDesignerStore;

        public UpdateSpellCardCommand(SpellCardViewModel SpellCardViewModel, CardDesignerStore cardDesignerStore)
        {
            _SpellCardViewModel = SpellCardViewModel;
            _cardDesignerStore = cardDesignerStore;
            _SpellCardViewModel.PropertyChanged += PropertyChangedEventHandle;
        }

        private void PropertyChangedEventHandle(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SpellCardViewModel.SelectedSpellCard))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _SpellCardViewModel.SelectedSpellCard != null;
        }

        public override void Execute(object parameter)
        {
            _cardDesignerStore.UpdateSpellCard(_SpellCardViewModel.SelectedSpellCard);
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