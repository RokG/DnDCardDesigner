using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CardDesigner.UI.ViewModels
{
    public partial class ViewModelBase : ObservableObject, IViewModelBase
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ViewModelType Type { get; set; } = ViewModelType.Unknown;
    }
}