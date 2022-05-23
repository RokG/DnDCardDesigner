using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System;
using CardDesigner.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesigner.UI.Commands
{
    public class AddCardCommand : CommandBase
    {
        private readonly CardCreatorViewModel _cardCreatorViewModel;
        private readonly CardDesignerStore _cardDesignerStore;

        public AddCardCommand(CardCreatorViewModel cardCreatorViewModel, CardDesignerStore cardDesignerStore)
        {
            _cardCreatorViewModel = cardCreatorViewModel;
            _cardDesignerStore = cardDesignerStore;
            _cardCreatorViewModel.PropertyChanged += PropertyChangedEventHandle;
        }

        private void PropertyChangedEventHandle(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CardCreatorViewModel.MagicShoolType))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _cardCreatorViewModel.MagicShoolType == MagicSchool.Abjuration && base.CanExecute(parameter);
            //return true;
        }

        public override void Execute(object? parameter)
        {
            Debug.Write("Something");
        }
    }
}