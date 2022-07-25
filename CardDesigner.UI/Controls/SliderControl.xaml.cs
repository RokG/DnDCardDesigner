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
    /// Interaction logic for SliderControl.xaml
    /// </summary>
    public partial class SliderControl : UserControl
    {
        public SliderControl()
        {
            InitializeComponent();
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(SliderControl), new PropertyMetadata(0.0));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(SliderControl), new PropertyMetadata(string.Empty));

        public object SelectedValueType
        {
            get { return (object)GetValue(SelectedValueTypeProperty); }
            set { SetValue(SelectedValueTypeProperty, value); }
        }

        public static readonly DependencyProperty SelectedValueTypeProperty =
            DependencyProperty.Register(nameof(SelectedValueType), typeof(object), typeof(SliderControl), new PropertyMetadata(null));

        public object ValueTypes
        {
            get { return (object)GetValue(ValueTypesProperty); }
            set { SetValue(ValueTypesProperty, value); }
        }

        public static readonly DependencyProperty ValueTypesProperty =
            DependencyProperty.Register(nameof(ValueTypes), typeof(object), typeof(SliderControl), new PropertyMetadata(null));

    }
}
