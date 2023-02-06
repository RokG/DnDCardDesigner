using CardDesigner.Domain.HelperModels;
using System.Windows;
using System.Windows.Controls;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for AbilityControl.xaml
    /// </summary>
    public partial class AbilityControl : UserControl
    {
        public AbilityControl()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent CheckChangedEvent =
         EventManager.RegisterRoutedEvent(nameof(CheckChangedEvent), RoutingStrategy.Bubble,
         typeof(RoutedEventHandler), typeof(AbilityControl));

        public event RoutedEventHandler CheckChanged
        {
            add => AddHandler(CheckChangedEvent, value);
            remove => RemoveHandler(CheckChangedEvent, value);
        }

        public static readonly RoutedEvent SliderChangedEvent =
         EventManager.RegisterRoutedEvent(nameof(SliderChangedEvent), RoutingStrategy.Bubble,
         typeof(RoutedEventHandler), typeof(AbilityControl));

        public event RoutedEventHandler SliderChanged
        {
            add => AddHandler(SliderChangedEvent, value);
            remove => RemoveHandler(SliderChangedEvent, value);
        }

        public AbilityModel Ability
        {
            get => (AbilityModel)GetValue(AbilityProperty);
            set => SetValue(AbilityProperty, value);
        }

        public static readonly DependencyProperty AbilityProperty =
            DependencyProperty.Register(nameof(Ability), typeof(AbilityModel), typeof(AbilityControl), new PropertyMetadata(new AbilityModel()));

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
