using CardDesigner.Domain.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
