using CardDesigner.Domain.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace CardDesigner.UI.ViewModels
{
    public partial class ViewModelBase : ObservableObject, IViewModelBase
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}