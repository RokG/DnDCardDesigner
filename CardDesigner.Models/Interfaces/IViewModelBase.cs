using System.ComponentModel;

namespace CardDesigner.Domain.Interfaces
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
        string Name { get; set; }
    }
}