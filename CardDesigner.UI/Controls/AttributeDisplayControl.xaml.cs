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
            DependencyProperty.Register(nameof(Attribute), typeof(AttributeModel), typeof(AttributeDisplayControl), new PropertyMetadata(new AttributeModel(), SetBonuses));

        public int CharacterProficiency
        {
            get => (int)GetValue(CharacterProficiencyProperty);
            set => SetValue(CharacterProficiencyProperty, value);
        }

        public static readonly DependencyProperty CharacterProficiencyProperty =
            DependencyProperty.Register(nameof(CharacterProficiency), typeof(int), typeof(AttributeDisplayControl), new PropertyMetadata(0, SetBonuses));

        private static void SetBonuses(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AttributeDisplayControl adc)
            {
                AttributeModel attribute = adc.Attribute;
                int attributeBonus = (attribute.Level - 10) / 2;

                adc.AttributeName = attribute.Type.ToString().Substring(0, 3).ToUpper();

                attribute.SavingThrowsBonus = attribute.HasSavingThrows ? adc.CharacterProficiency + attributeBonus : attributeBonus;

                if (attribute.Skills != null)
                {
                    if (attribute.Skills.Any())
                    {
                        foreach (SkillModel skill in attribute.Skills)
                        {
                            if (skill.IsProficient)
                            {
                                skill.Bonus = adc.CharacterProficiency + attributeBonus;
                            }
                            else if (skill.IsExpert)
                            {
                                skill.Bonus = 2 * adc.CharacterProficiency + attributeBonus;
                            }
                            else if (skill.IsBasic)
                            {
                                skill.Bonus = attributeBonus;
                            }
                        }
                    }
                }

                adc.Attribute= attribute;
            }
        }
    }
}
