using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Services;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.Commands;
using System.Windows.Input;

namespace CardDesigner.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly CardDesignerStore _cardDesignerStore;

        public ICommand CardCreatorNavigationCommand { get; }
        public ICommand CardDisplayNavigationCommand { get; }

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

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public IViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
    }
}