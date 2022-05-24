using CardDesigner.Domain.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CardDesigner.UI.ViewModels
{
    public class ViewModelBase : IViewModelBase
    {
        public string Name { get; set; } = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }
    }
}