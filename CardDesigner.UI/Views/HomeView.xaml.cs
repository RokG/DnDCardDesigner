using CardDesigner.Domain.HelperModels;
using CardDesigner.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CardDesigner.UI.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (sender is TreeView treeView)
            {
                if (treeView.DataContext is HomeViewModel hvm)
                {
                    hvm.SetSelectedItem(((TreeItemModel)treeView.SelectedItem).Item);
                }
            }
        }
    }
}
