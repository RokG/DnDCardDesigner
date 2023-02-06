using CardDesigner.Domain.Models;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for AttributesDisplayControl.xaml
    /// </summary>
    public partial class AttributesDisplayControl : UserControl
    {
        public AttributesDisplayControl()
        {
            InitializeComponent();
        }

        public CharacterAttributesModel Attributes
        {
            get => (CharacterAttributesModel)GetValue(AttributesProperty);
            set => SetValue(AttributesProperty, value);
        }

        public static readonly DependencyProperty AttributesProperty =
            DependencyProperty.Register(nameof(Attributes), typeof(CharacterAttributesModel), typeof(AttributesDisplayControl), new PropertyMetadata(new CharacterAttributesModel()));

        public bool SwitchAbilityValueBonus
        {
            get { return (bool)GetValue(SwitchAbilityValueBonusProperty); }
            set { SetValue(SwitchAbilityValueBonusProperty, value); }
        }

        public static readonly DependencyProperty SwitchAbilityValueBonusProperty =
            DependencyProperty.Register(nameof(SwitchAbilityValueBonus), typeof(bool), typeof(AttributesDisplayControl), new PropertyMetadata(false));

    }
}
