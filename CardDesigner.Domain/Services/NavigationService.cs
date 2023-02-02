using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using System;

namespace CardDesigner.Domain.Services
{
    public class NavigationService<TViewModel> where TViewModel : IViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
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