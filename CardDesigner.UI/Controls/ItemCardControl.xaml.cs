using CardDesigner.Domain.Models;
using System.Windows;
using System.Windows.Controls;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for ItemCardControl.xaml
    /// </summary>
    public partial class ItemCardControl : UserControl
    {
        public ItemCardControl()
        {
            InitializeComponent();
        }

        #region Properties

        public bool IsEditable
        {
            get => (bool)GetValue(IsEditableProperty);
            set => SetValue(IsEditableProperty, value);
        }

        public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.Register(nameof(IsEditable), typeof(bool), typeof(ItemCardControl), new PropertyMetadata(false));

        public ItemCardModel ItemCard
        {
            get => (ItemCardModel)GetValue(ItemCardProperty);
            set => SetValue(ItemCardProperty, value);
        }

        public static readonly DependencyProperty ItemCardProperty =
            DependencyProperty.Register(nameof(ItemCard), typeof(ItemCardModel), typeof(ItemCardControl), new PropertyMetadata(null));

        #endregion


    }
}
