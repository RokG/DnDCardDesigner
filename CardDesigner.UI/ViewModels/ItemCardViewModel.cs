using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public partial class ItemCardViewModel : ViewModelBase
    {
        #region Private fields


        #endregion

        #region Properties
        [ObservableProperty]
        private string cardName;

        #endregion

        #region Actions, Events, Commands

        public ICommand DoNavigateCommand { get; }

        #endregion Actions, Events, Commands

        #region Constructor

        public ItemCardViewModel(CardDesignerStore cardDesignerStore)
        {
            Name = Regex.Replace(nameof(ItemCardViewModel).Replace("ViewModel", ""), "(\\B[A-Z])", " $1");
        }

        #endregion

        #region Private methods

        #endregion

        #region Public methods

        public static ItemCardViewModel LoadViewModel(CardDesignerStore cardDesignerStore)
        {
            return new(cardDesignerStore);
        }

        #endregion
    }
}