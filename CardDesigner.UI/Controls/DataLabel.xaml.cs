using System.Windows;
using System.Windows.Controls;

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

        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(DataLabel), new PropertyMetadata(Orientation.Horizontal));

        public string ValueName
        {
            get => (string)GetValue(ValueNameProperty);
            set => SetValue(ValueNameProperty, value);
        }
        public static readonly DependencyProperty ValueNameProperty =
            DependencyProperty.Register(nameof(ValueName), typeof(string), typeof(DataLabel), new PropertyMetadata(string.Empty));

        public string Value1
        {
            get => (string)GetValue(Value1Property);
            set => SetValue(Value1Property, value);
        }

        public static readonly DependencyProperty Value1Property =
            DependencyProperty.Register(nameof(Value1), typeof(string), typeof(DataLabel), new PropertyMetadata(string.Empty));

        public string Value2
        {
            get => (string)GetValue(Value2Property);
            set => SetValue(Value3Property, value);
        }

        public static readonly DependencyProperty Value2Property =
            DependencyProperty.Register(nameof(Value2), typeof(string), typeof(DataLabel), new PropertyMetadata(string.Empty));

        public string Value3
        {
            get => (string)GetValue(Value3Property);
            set => SetValue(Value3Property, value);
        }

        public static readonly DependencyProperty Value3Property =
            DependencyProperty.Register(nameof(Value3), typeof(string), typeof(DataLabel), new PropertyMetadata(string.Empty));

    }
}
