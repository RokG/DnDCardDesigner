using CardDesigner.Domain.Models;
using System.Windows;
using System.Windows.Controls;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for AttributeControl.xaml
    /// </summary>
    public partial class AttributesControl : UserControl
    {
        public AttributesControl()
        {
            InitializeComponent();
        }

        public CharacterAttributesModel Attributes
        {
            get => (CharacterAttributesModel)GetValue(AttributesProperty);
            set => SetValue(AttributesProperty, value);
        }

        public static readonly DependencyProperty AttributesProperty =
            DependencyProperty.Register(nameof(Attributes), typeof(CharacterAttributesModel), typeof(AttributesControl), new PropertyMetadata(null));

        private void AttributeControl_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (sender is AttributeControl attributeControl)
            {
                if (attributeControl.Attribute != null)
                {
                    Attributes.SetAttribute(attributeControl.Attribute);
                }
            }
        }
    }
}
