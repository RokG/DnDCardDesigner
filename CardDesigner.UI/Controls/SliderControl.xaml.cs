using System.Windows;
using System.Windows.Controls;

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
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(SliderControl), new PropertyMetadata(0.0));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(SliderControl), new PropertyMetadata(string.Empty));

        public object SelectedValueType
        {
            get => GetValue(SelectedValueTypeProperty);
            set => SetValue(SelectedValueTypeProperty, value);
        }

        public static readonly DependencyProperty SelectedValueTypeProperty =
            DependencyProperty.Register(nameof(SelectedValueType), typeof(object), typeof(SliderControl), new PropertyMetadata(null));

        public object ValueTypes
        {
            get => GetValue(ValueTypesProperty);
            set => SetValue(ValueTypesProperty, value);
        }
        public static readonly DependencyProperty ValueTypesProperty =
            DependencyProperty.Register(nameof(ValueTypes), typeof(object), typeof(SliderControl), new PropertyMetadata(null));

        public object SelectedUnitType
        {
            get => GetValue(SelectedUnitTypeProperty);
            set => SetValue(SelectedUnitTypeProperty, value);
        }

        public static readonly DependencyProperty SelectedUnitTypeProperty =
            DependencyProperty.Register(nameof(SelectedUnitType), typeof(object), typeof(SliderControl), new PropertyMetadata(null));


        public object UnitTypes
        {
            get => GetValue(UnitTypesProperty);
            set => SetValue(UnitTypesProperty, value);
        }
        public static readonly DependencyProperty UnitTypesProperty =
            DependencyProperty.Register(nameof(UnitTypes), typeof(object), typeof(SliderControl), new PropertyMetadata(null));

    }
}
