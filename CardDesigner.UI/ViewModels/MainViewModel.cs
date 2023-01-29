using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Services;
using CardDesigner.Domain.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CardDesigner.UI.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        #region Private fields

        private readonly NavigationStore _navigationStore;
        private readonly CardDesignerStore _cardDesignerStore;

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
        public MainViewModel(NavigationStore navigationStore,
            CardDesignerStore cardDesignerStore,
            NavigationService<HomeViewModel> homeViewModelNavigationService,
            NavigationService<ItemCardViewModel> itemCardNavigationService,
            NavigationService<SpellCardViewModel> spellCardNavigationService,
            NavigationService<CardDecksViewModel> spellDeckNavigationService,
            NavigationService<CharacterViewModel> characterViewNavigationService,
            NavigationService<CardDesignViewModel> cardDesignViewNavigationService)
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
            // TODO: check how to do this better?
            switch (viewModelType)
            {
                case "HomeViewModel":
                    CurrentViewModel = new HomeViewModel(_cardDesignerStore);
                    break;
                case "SpellCardViewModel":
                    CurrentViewModel = new SpellCardViewModel(_cardDesignerStore);
                    break;
                case "ItemCardViewModel":
                    CurrentViewModel = new ItemCardViewModel(_cardDesignerStore);
                    break;
                case "CardDecksViewModel":
                    CurrentViewModel = new CardDecksViewModel(_cardDesignerStore);
                    break;
                case "CharacterViewModel":
                    CurrentViewModel = new CharacterViewModel(_cardDesignerStore);
                    break;
                case "CardDesignViewModel":
                    CurrentViewModel = new CardDesignViewModel(_cardDesignerStore);
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}