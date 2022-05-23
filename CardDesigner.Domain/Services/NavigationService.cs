using CardDesigner.Domain;
using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Stores;
using System;

namespace CardDesigner.Domain.Services
{
    public class NavigationService
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<IViewModelBase> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<IViewModelBase> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}