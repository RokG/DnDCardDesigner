using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public class CharacterViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CharacterModel _character;

        #endregion

        #region Properties

        private string _cardName;
        public string CardName
        {
            get => _cardName;
            set => SetProperty(ref _cardName, value);
        }

        #endregion

        #region Actions, Events, Commands

        public ICommand DoNavigateCommand { get; }

        #endregion Actions, Events, Commands

        #region Constructor

        public CharacterViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = nameof(CharacterViewModel).Replace("ViewModel", "");
        }

        #endregion

        #region Private methods

        #endregion

        #region Public methods

        public static CharacterViewModel LoadViewModel(CardDesignerStore cardDesignerStore)
        {
            return new(cardDesignerStore);
        }

        #endregion
    }
}
