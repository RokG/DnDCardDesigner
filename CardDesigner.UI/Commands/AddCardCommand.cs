﻿using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;

namespace CardDesigner.UI.Commands
{
    public class AddCardCommand : CommandBase
    {
        private readonly SpellCardViewModel _cardCreatorViewModel;
        private readonly CardDesignerStore _cardDesignerStore;

        public AddCardCommand(SpellCardViewModel cardCreatorViewModel, CardDesignerStore cardDesignerStore)
        {
            _cardCreatorViewModel = cardCreatorViewModel;
            _cardDesignerStore = cardDesignerStore;
            _cardCreatorViewModel.PropertyChanged += PropertyChangedEventHandle;
        }

        private void PropertyChangedEventHandle(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SpellCardViewModel.MagicSchoolType))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _cardCreatorViewModel.MagicSchoolType == MagicSchool.Alteration && base.CanExecute(parameter);
            //return true;
        }

        public override void Execute(object parameter)
        {
            _cardCreatorViewModel.AddRandomCard();
            _cardDesignerStore.CreateSpellCard(_cardCreatorViewModel.SelectedSpellCard);
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