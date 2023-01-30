using CardDesigner.Domain.Models;
using System.Windows;
using System.Windows.Controls;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for SpellCardControl.xaml
    /// </summary>
    public partial class SpellCardControl : UserControl
    {
        public SpellCardControl()
        {
            InitializeComponent();
        }

        #region Properties

        public SpellDeckDesignModel CardDesign
        {
            get => (SpellDeckDesignModel)GetValue(CardDesignProperty);
            set => SetValue(CardDesignProperty, value);
        }

        public static readonly DependencyProperty CardDesignProperty =
            DependencyProperty.Register(nameof(CardDesign), typeof(SpellDeckDesignModel), typeof(SpellCardControl), new PropertyMetadata(new SpellDeckDesignModel()));

        public bool IsEditable
        {
            get => (bool)GetValue(IsEditableProperty);
            set => SetValue(IsEditableProperty, value);
        }

        public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.Register(nameof(IsEditable), typeof(bool), typeof(SpellCardControl), new PropertyMetadata(false));

        public SpellCardModel SpellCard
        {
            get => (SpellCardModel)GetValue(SpellCardProperty);
            set => SetValue(SpellCardProperty, value);
        }

        public static readonly DependencyProperty SpellCardProperty =
            DependencyProperty.Register(nameof(SpellCard), typeof(SpellCardModel), typeof(SpellCardControl), new PropertyMetadata(null));

        #endregion
    }
}
