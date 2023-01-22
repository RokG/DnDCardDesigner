using CardDesigner.Domain.Models;
using System;
using System.Collections.Generic;
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
            get { return (bool)GetValue(IsEditableProperty); }
            set { SetValue(IsEditableProperty, value); }
        }

        public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.Register(nameof(IsEditable), typeof(bool), typeof(ItemCardControl), new PropertyMetadata(false));

        public ItemCardModel ItemCard
        {
            get { return (ItemCardModel)GetValue(ItemCardProperty); }
            set { SetValue(ItemCardProperty, value); }
        }

        public static readonly DependencyProperty ItemCardProperty =
            DependencyProperty.Register(nameof(ItemCard), typeof(ItemCardModel), typeof(ItemCardControl), new PropertyMetadata(null));

        #endregion

        private void Image_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}
