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
    /// Interaction logic for SpellCardControl.xaml
    /// </summary>
    public partial class SpellCardControl : UserControl
    {
        public SpellCardControl()
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
            DependencyProperty.Register(nameof(IsEditable), typeof(bool), typeof(SpellCardControl), new PropertyMetadata(false));

        public SpellCardModel SpellCard
        {
            get { return (SpellCardModel)GetValue(SpellCardProperty); }
            set { SetValue(SpellCardProperty, value); }
        }

        public static readonly DependencyProperty SpellCardProperty =
            DependencyProperty.Register(nameof(SpellCard), typeof(SpellCardModel), typeof(SpellCardControl), new PropertyMetadata(null));

        #endregion
    }
}
