using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Services;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.Commands;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Private fields

        private readonly NavigationStore _navigationStore;
        private readonly CardDesignerStore _cardDesignerStore;

        #endregion

        #region Properties

        public IViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        #endregion

        #region Actions, Commands, Events

        public ICommand CharacterNavigationCommand { get; }
        public ICommand SpellCardNavigationCommand { get; }
        public ICommand ItemCardNavigationCommand { get; }

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
            NavigationService<ItemCardViewModel> cardDisplayNavigationService,
            NavigationService<SpellCardViewModel> cardCreatorNavigationService,
            NavigationService<CharacterViewModel> characterNavigationService)
        {
            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            ItemCardNavigationCommand = new NavigateCommand<ItemCardViewModel>(cardDisplayNavigationService);
            SpellCardNavigationCommand = new NavigateCommand<SpellCardViewModel>(cardCreatorNavigationService);
            CharacterNavigationCommand = new NavigateCommand<CharacterViewModel>(characterNavigationService);
        }

        #endregion

        #region Private methods

        private void OnCurrentViewModelChanged()
        {
            RaisePropertyChanged(nameof(CurrentViewModel));
        }

        #endregion
    }
}