using CardDesigner.Domain.Interfaces;
using System;

namespace InvoiceMe.Domain.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;

        private IViewModelBase _currentViewModel;

        public IViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}