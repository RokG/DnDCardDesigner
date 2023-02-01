using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CardDesigner.UI.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        #region Private fields

        private readonly CardDesignerStore _cardDesignerStore;
        private readonly NavigationStore _navigationStore;

        #endregion

        #region Properties

        [ObservableProperty]
        public IViewModelBase currentViewModel;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="cardDesignerStore"></param>
        /// <param name="cardDisplayNavigationService"></param>
        /// <param name="cardCreatorNavigationService"></param>
        public MainViewModel(NavigationStore navigationStore, CardDesignerStore cardDesignerStore)
        {
            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;

            ChangeViewModelCommand.Execute("HomeViewModel");
        }

        #endregion

        #region Private methods

        [RelayCommand]
        private void ChangeViewModel(string viewModelType)
        {
            StoreCurrentViewModelSelections();
            // TODO: check how to do this better?
            switch (viewModelType)
            {
                case "HomeViewModel":
                    CurrentViewModel = new HomeViewModel(_cardDesignerStore, _navigationStore);
                    break;
                case "SpellCardViewModel":
                    CurrentViewModel = new SpellCardViewModel(_cardDesignerStore, _navigationStore);
                    break;
                case "ItemCardViewModel":
                    CurrentViewModel = new ItemCardViewModel(_cardDesignerStore, _navigationStore);
                    break;
                case "CardDecksViewModel":
                    CurrentViewModel = new CardDecksViewModel(_cardDesignerStore, _navigationStore);
                    break;
                case "CharacterViewModel":
                    CurrentViewModel = new CharacterViewModel(_cardDesignerStore, _navigationStore);
                    break;
                case "CardDesignViewModel":
                    CurrentViewModel = new CardDesignViewModel(_cardDesignerStore, _navigationStore);
                    break;
                default:
                    break;
            }
        }

        private void StoreCurrentViewModelSelections()
        {
            //switch (CurrentViewModel)
            //{
            //    case CardDesignViewModel cardDesign:
            //        _navigationStore.SelectedSpellDeckDesign = cardDesign.
            //        break;
            //    default:
            //        break;
            //}

            _navigationStore.CurrentViewModel = CurrentViewModel;

        }

        #endregion
    }
}