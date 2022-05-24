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

        public ICommand CardCreatorNavigationCommand { get; }
        public ICommand CardDisplayNavigationCommand { get; }

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
            NavigationService<CardDisplayViewModel> cardDisplayNavigationService,
            NavigationService<CardCreatorViewModel> cardCreatorNavigationService)
        {
            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            CardDisplayNavigationCommand = new NavigateCommand<CardDisplayViewModel>(cardDisplayNavigationService);
            CardCreatorNavigationCommand = new NavigateCommand<CardCreatorViewModel>(cardCreatorNavigationService);
        }

        #endregion

        #region Private methods

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        #endregion
    }
}