using CardDesigner.Domain.Models;
using System.Windows;
using System.Windows.Controls;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for AbilityControl.xaml
    /// </summary>
    public partial class AbilitiesControl : UserControl
    {
        public AbilitiesControl()
        {
            InitializeComponent();
        }

        public CharacterAbilitiesModel Abilities
        {
            get => (CharacterAbilitiesModel)GetValue(AbilitiesProperty);
            set => SetValue(AbilitiesProperty, value);
        }

        public static readonly DependencyProperty AbilitiesProperty =
            DependencyProperty.Register(nameof(Abilities), typeof(CharacterAbilitiesModel), typeof(AbilitiesControl), new PropertyMetadata(new CharacterAbilitiesModel()));

        private void AbilityControl_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (sender is AbilityControl attributeControl)
            {
                if (attributeControl.Ability != null)
                {
                    Abilities.SetAbility(attributeControl.Ability);
                }
            }
        }
    }
}
