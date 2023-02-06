using System.Windows;
using System.Windows.Controls;

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
            get => (string)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register(nameof(TextColor), typeof(string), typeof(IconWithPropertyControl), new PropertyMetadata("#FF000000"));

        public string IconColorFooter
        {
            get => (string)GetValue(IconColorProperty);
            set => SetValue(IconColorProperty, value);
        }

        public static readonly DependencyProperty IconColorProperty =
            DependencyProperty.Register(nameof(IconColorFooter), typeof(string), typeof(IconWithPropertyControl), new PropertyMetadata("#FF000000"));

        public object Icon
        {
            get => GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(object), typeof(IconWithPropertyControl), new PropertyMetadata(null));

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(IconWithPropertyControl), new PropertyMetadata(0.0));

        public string Unit
        {
            get => (string)GetValue(UnitProperty);
            set => SetValue(UnitProperty, value);
        }

        public static readonly DependencyProperty UnitProperty =
            DependencyProperty.Register("Unit", typeof(string), typeof(IconWithPropertyControl), new PropertyMetadata(string.Empty));
    }
}
