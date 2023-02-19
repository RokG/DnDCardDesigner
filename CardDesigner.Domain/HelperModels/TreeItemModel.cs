using System.Collections.ObjectModel;

namespace CardDesigner.Domain.HelperModels
{
    public class TreeItemModel
    {
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public object Item { get; set; }
        public object Property { get; set; } 
        public ObservableCollection<TreeItemModel> Items { get; set; } = new ObservableCollection<TreeItemModel>();
        public bool IsSelected { get; set; } = false;
        public bool IsExpanded { get; set; } = false;
    }
}
