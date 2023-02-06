using CardDesigner.Domain.HelperModels;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for AbilityDisplayControl.xaml
    /// </summary>
    public partial class AbilityDisplayControl : UserControl
    {
        public AbilityDisplayControl()
        {
            InitializeComponent();
        }

        public string AbilityName
        {
            get { return (string)GetValue(AbilityNameProperty); }
            set { SetValue(AbilityNameProperty, value); }
        }

        public static readonly DependencyProperty AbilityNameProperty =
            DependencyProperty.Register(nameof(AbilityName), typeof(string), typeof(AbilityDisplayControl), new PropertyMetadata(string.Empty));

        public AbilityModel Ability
        {
            get => (AbilityModel)GetValue(AbilityProperty);
            set => SetValue(AbilityProperty, value);
        }

        public static readonly DependencyProperty AbilityProperty =
            DependencyProperty.Register(nameof(Ability), typeof(AbilityModel), typeof(AbilityDisplayControl), new PropertyMetadata(new AbilityModel()));

        public bool SwitchAbilityValueBonus
        {
            get { return (bool)GetValue(SwitchAbilityValueBonusProperty); }
            set { SetValue(SwitchAbilityValueBonusProperty, value); }
        }

        public static readonly DependencyProperty SwitchAbilityValueBonusProperty =
            DependencyProperty.Register(nameof(SwitchAbilityValueBonus), typeof(bool), typeof(AbilityDisplayControl), new PropertyMetadata(false));

    }
}
