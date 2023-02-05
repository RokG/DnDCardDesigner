using CardDesigner.Domain.HelperModels;
using System.Windows;
using System.Windows.Controls;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for AttributeControl.xaml
    /// </summary>
    public partial class AttributeControl : UserControl
    {
        public AttributeControl()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent CheckChangedEvent =
         EventManager.RegisterRoutedEvent(nameof(CheckChangedEvent), RoutingStrategy.Bubble,
         typeof(RoutedEventHandler), typeof(AttributeControl));

        public event RoutedEventHandler CheckChanged
        {
            add => AddHandler(CheckChangedEvent, value);
            remove => RemoveHandler(CheckChangedEvent, value);
        }

        public static readonly RoutedEvent SliderChangedEvent =
         EventManager.RegisterRoutedEvent(nameof(SliderChangedEvent), RoutingStrategy.Bubble,
         typeof(RoutedEventHandler), typeof(AttributeControl));

        public event RoutedEventHandler SliderChanged
        {
            add => AddHandler(SliderChangedEvent, value);
            remove => RemoveHandler(SliderChangedEvent, value);
        }

        public AttributeModel Attribute
        {
            get => (AttributeModel)GetValue(AttributeProperty);
            set => SetValue(AttributeProperty, value);
        }

        public static readonly DependencyProperty AttributeProperty =
            DependencyProperty.Register(nameof(Attribute), typeof(AttributeModel), typeof(AttributeControl), new PropertyMetadata(new AttributeModel()));

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(CheckChangedEvent));
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RaiseEvent(new RoutedEventArgs(SliderChangedEvent));

        }
    }
}
