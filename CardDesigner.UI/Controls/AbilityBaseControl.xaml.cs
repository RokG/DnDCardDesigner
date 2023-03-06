using CardDesigner.Domain.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for AbilityBaseControl.xaml
    /// </summary>
    public partial class AbilityBaseControl : UserControl
    {
        public AbilityBaseControl()
        {
            InitializeComponent();
        }

        public string AbilityName
        {
            get => (string)GetValue(AbilityNameProperty);
            set => SetValue(AbilityNameProperty, value);
        }

        public static readonly DependencyProperty AbilityNameProperty =
            DependencyProperty.Register(nameof(AbilityName), typeof(string), typeof(AbilityBaseControl), new PropertyMetadata(string.Empty));

        public int AbilityLevel
        {
            get => (int)GetValue(AbilityLevelProperty);
            set => SetValue(AbilityLevelProperty, value);
        }

        public static readonly DependencyProperty AbilityLevelProperty =
            DependencyProperty.Register(nameof(AbilityLevel), typeof(int), typeof(AbilityBaseControl), new PropertyMetadata(0));

        public int AbilityLevelBonus
        {
            get => (int)GetValue(AbilityLevelBonusProperty);
            set => SetValue(AbilityLevelBonusProperty, value);
        }

        public static readonly DependencyProperty AbilityLevelBonusProperty =
            DependencyProperty.Register(nameof(AbilityLevelBonus), typeof(int), typeof(AbilityBaseControl), new PropertyMetadata(0));

        public bool SwitchAbilityValueBonus
        {
            get => (bool)GetValue(SwitchAbilityValueBonusProperty);
            set => SetValue(SwitchAbilityValueBonusProperty, value);
        }

        public static readonly DependencyProperty SwitchAbilityValueBonusProperty =
            DependencyProperty.Register(nameof(SwitchAbilityValueBonus), typeof(bool), typeof(AbilityBaseControl), new PropertyMetadata(false));


    }
}
