using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Stores;
using InvoiceMe.Domain.Stores;
using System.ComponentModel;

namespace CardDesigner.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public IViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
    }
}