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
    /// Interaction logic for IconWithProperty.xaml
    /// </summary>
    public partial class IconWithPropertyControl : UserControl
    {
        public IconWithPropertyControl()
        {
            InitializeComponent();
        }

        public string TextColor
        {
            get { return (string)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register(nameof(TextColor), typeof(string), typeof(IconWithPropertyControl), new PropertyMetadata("#FF000000"));

        public string IconColor
        {
            get { return (string)GetValue(IconColorProperty); }
            set { SetValue(IconColorProperty, value); }
        }

        public static readonly DependencyProperty IconColorProperty =
            DependencyProperty.Register(nameof(IconColor), typeof(string), typeof(IconWithPropertyControl), new PropertyMetadata("#FF000000"));

        public object Icon
        {
            get { return (object)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(object), typeof(IconWithPropertyControl), new PropertyMetadata(null));

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(IconWithPropertyControl), new PropertyMetadata(0.0));

        public string Unit
        {
            get { return (string)GetValue(UnitProperty); }
            set { SetValue(UnitProperty, value); }
        }

        public static readonly DependencyProperty UnitProperty =
            DependencyProperty.Register("Unit", typeof(string), typeof(IconWithPropertyControl), new PropertyMetadata(string.Empty));
    }
}
