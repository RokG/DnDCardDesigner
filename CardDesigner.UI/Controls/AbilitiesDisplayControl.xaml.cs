using CardDesigner.Domain.Models;
using System.Windows;
using System.Windows.Controls;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for AbilitiesDisplayControl.xaml
    /// </summary>
    public partial class AbilitiesDisplayControl : UserControl
    {
        public AbilitiesDisplayControl()
        {
            InitializeComponent();
        }

        public CharacterAbilitiesModel Abilities
        {
            get => (CharacterAbilitiesModel)GetValue(AbilitiesProperty);
            set => SetValue(AbilitiesProperty, value);
        }

        public static readonly DependencyProperty AbilitiesProperty =
            DependencyProperty.Register(nameof(Abilities), typeof(CharacterAbilitiesModel), typeof(AbilitiesDisplayControl), new PropertyMetadata(new CharacterAbilitiesModel()));

        public bool SwitchAbilityValueBonus
        {
            get { return (bool)GetValue(SwitchAbilityValueBonusProperty); }
            set { SetValue(SwitchAbilityValueBonusProperty, value); }
        }

        public static readonly DependencyProperty SwitchAbilityValueBonusProperty =
            DependencyProperty.Register(nameof(SwitchAbilityValueBonus), typeof(bool), typeof(AbilitiesDisplayControl), new PropertyMetadata(false));

    }
}
