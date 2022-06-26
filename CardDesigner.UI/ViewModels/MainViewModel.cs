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
        public ICommand SpellDeckNavigationCommand { get; }
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
            NavigationService<ItemCardViewModel> itemCardNavigationService,
            NavigationService<SpellCardViewModel> spellCardNavigationService,
            NavigationService<SpellDeckViewModel> spellDeckNavigationService,
            NavigationService<CharacterViewModel> characterViewNavigationService)
        {
            _cardDesignerStore = cardDesignerStore;
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            ItemCardNavigationCommand = new NavigateCommand<ItemCardViewModel>(itemCardNavigationService);
            SpellCardNavigationCommand = new NavigateCommand<SpellCardViewModel>(spellCardNavigationService);
            SpellDeckNavigationCommand = new NavigateCommand<SpellDeckViewModel>(spellDeckNavigationService);
            CharacterNavigationCommand = new NavigateCommand<CharacterViewModel>(characterViewNavigationService);
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