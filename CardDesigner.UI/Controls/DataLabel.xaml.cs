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
    /// Interaction logic for DataLabel.xaml
    /// </summary>
    public partial class DataLabel : UserControl
    {
        public DataLabel()
        {
            InitializeComponent();
        }

        public string ValueName
        {
            get { return (string)GetValue(ValueNameProperty); }
            set { SetValue(ValueNameProperty, value); }
        }
        public static readonly DependencyProperty ValueNameProperty =
            DependencyProperty.Register(nameof(ValueName), typeof(string), typeof(DataLabel), new PropertyMetadata(string.Empty));

        public string Value1
        {
            get { return (string)GetValue(Value1Property); }
            set { SetValue(Value1Property, value); }
        }

        public static readonly DependencyProperty Value1Property =
            DependencyProperty.Register(nameof(Value1), typeof(string), typeof(DataLabel), new PropertyMetadata(string.Empty));

        public string Value2
        {
            get { return (string)GetValue(Value2Property); }
            set { SetValue(Value3Property, value); }
        }

        public static readonly DependencyProperty Value2Property =
            DependencyProperty.Register(nameof(Value2), typeof(string), typeof(DataLabel), new PropertyMetadata(string.Empty));

        public string Value3
        {
            get { return (string)GetValue(Value3Property); }
            set { SetValue(Value3Property, value); }
        }

        public static readonly DependencyProperty Value3Property =
            DependencyProperty.Register(nameof(Value3), typeof(string), typeof(DataLabel), new PropertyMetadata(string.Empty));

    }
}
