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
            if (e.PropertyName == nameof(SpellCardViewModel.SpellCardName))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            string cardName = _SpellCardViewModel.SpellCardName;
            return cardName != null
                && cardName != string.Empty
                && !_SpellCardViewModel.AllSpellCards.Where(c => c.Name == cardName).Any();

        }

        public override void Execute(object parameter)
        {
            _cardDesignerStore.CreateSpellCard(new SpellCardModel() { Name = _SpellCardViewModel.SpellCardName });
        }

    }
}