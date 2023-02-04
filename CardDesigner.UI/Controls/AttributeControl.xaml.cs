using CardDesigner.Domain.Enums;
using CardDesigner.Domain.HelperModels;
using CardDesigner.Domain.Models;
using System.Diagnostics;
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
            add { AddHandler(CheckChangedEvent, value); }
            remove { RemoveHandler(CheckChangedEvent, value); }
        }

        public AttributeModel Attribute
        {
            get => (AttributeModel)GetValue(AttributeProperty);
            set => SetValue(AttributeProperty, value);
        }

        public static readonly DependencyProperty AttributeProperty =
            DependencyProperty.Register(nameof(Attribute), typeof(AttributeModel), typeof(AttributeControl), new PropertyMetadata(null));

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("C");
            RaiseEvent(new RoutedEventArgs(CheckChangedEvent));
        }
    }
}
