using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.RegularExpressions;

namespace CardDesigner.UI.ViewModels
{
    public partial class ViewModelBase : ObservableObject, IViewModelBase
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ViewModelType Type { get; set; } = ViewModelType.Unknown;

        [GeneratedRegex("(\\B[A-Z])")]
        public static partial Regex ModuleNameRegex();
    }
}