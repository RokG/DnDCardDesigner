using CardDesigner.Domain.HelperModels;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for AttributeDisplayControl.xaml
    /// </summary>
    public partial class AttributeDisplayControl : UserControl
    {
        public AttributeDisplayControl()
        {
            InitializeComponent();
        }

        public string AttributeName
        {
            get { return (string)GetValue(AttributeNameProperty); }
            set { SetValue(AttributeNameProperty, value); }
        }

        public static readonly DependencyProperty AttributeNameProperty =
            DependencyProperty.Register(nameof(AttributeName), typeof(string), typeof(AttributeDisplayControl), new PropertyMetadata(string.Empty));

        public AttributeModel Attribute
        {
            get => (AttributeModel)GetValue(AttributeProperty);
            set => SetValue(AttributeProperty, value);
        }

        public static readonly DependencyProperty AttributeProperty =
            DependencyProperty.Register(nameof(Attribute), typeof(AttributeModel), typeof(AttributeDisplayControl), new PropertyMetadata(new AttributeModel()));

        public bool SwitchAbilityValueBonus
        {
            get { return (bool)GetValue(SwitchAbilityValueBonusProperty); }
            set { SetValue(SwitchAbilityValueBonusProperty, value); }
        }

        public static readonly DependencyProperty SwitchAbilityValueBonusProperty =
            DependencyProperty.Register(nameof(SwitchAbilityValueBonus), typeof(bool), typeof(AttributeDisplayControl), new PropertyMetadata(false));

    }
}
