using CardDesigner.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for ItemTransferControl.xaml
    /// </summary>
    public partial class ItemTransferControl : UserControl
    {
        public ItemTransferControl()
        {
            InitializeComponent();
        }

        public ICommand AddCommand
        {
            get => (ICommand)GetValue(AddCommandProperty);
            set => SetValue(AddCommandProperty, value);
        }

        public static readonly DependencyProperty AddCommandProperty =
            DependencyProperty.Register(nameof(AddCommand), typeof(ICommand), typeof(ItemTransferControl), new PropertyMetadata(null));

        public ICommand RemoveCommand
        {
            get => (ICommand)GetValue(RemoveCommandProperty);
            set => SetValue(RemoveCommandProperty, value);
        }

        public static readonly DependencyProperty RemoveCommandProperty =
            DependencyProperty.Register(nameof(RemoveCommand), typeof(ICommand), typeof(ItemTransferControl), new PropertyMetadata(null));

        public ObservableCollection<ISelectableItem> ItemsSource
        {
            get => (ObservableCollection<ISelectableItem>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable<ISelectableItem>), typeof(ItemTransferControl), new PropertyMetadata(null));
        public ObservableCollection<ISelectableItem> ItemsDestination
        {
            get => (ObservableCollection<ISelectableItem>)GetValue(ItemsDestinationProperty);
            set => SetValue(ItemsDestinationProperty, value);
        }

        public static readonly DependencyProperty ItemsDestinationProperty =
            DependencyProperty.Register(nameof(ItemsDestination), typeof(IEnumerable<ISelectableItem>), typeof(ItemTransferControl), new PropertyMetadata(null));

    }
}
