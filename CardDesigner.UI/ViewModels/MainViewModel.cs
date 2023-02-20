using CardDesigner.Domain.Enums;
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

            ChangeViewModelCommand.Execute(ViewModelType.Home);

            _navigationStore.CurrentViewModelChanged += ChangeViewModel;
        }

        #endregion

        #region Private methods

        [RelayCommand]
        private void ChangeViewModel(ViewModelType viewModelType)
        {
            switch (viewModelType)
            {
                case ViewModelType.Home:
                    CurrentViewModel = new HomeViewModel(_cardDesignerStore, _navigationStore);
                    break;
                case ViewModelType.SpellCardCreator:
                    CurrentViewModel = new SpellCardViewModel(_cardDesignerStore, _navigationStore);
                    break;
                case ViewModelType.CharacterCardCreator:
                    CurrentViewModel = new CharacterCardViewModel(_cardDesignerStore, _navigationStore);
                    break;
                case ViewModelType.ItemCardCreator:
                    CurrentViewModel = new ItemCardViewModel(_cardDesignerStore, _navigationStore);
                    break;
                case ViewModelType.DeckCreator:
                    CurrentViewModel = new CardDecksViewModel(_cardDesignerStore, _navigationStore);
                    break;
                case ViewModelType.CharacterCreator:
                    CurrentViewModel = new CharacterViewModel(_cardDesignerStore, _navigationStore);
                    break;
                case ViewModelType.DeckDesigner:
                    CurrentViewModel = new DeckDesignViewModel(_cardDesignerStore, _navigationStore);
                    break;
                default:
                    break;
            }
            _navigationStore.CurrentViewModel = CurrentViewModel;
        }

        #endregion
    }
}