using CardDesigner.Domain.Models;
using System.Windows;
using System.Windows.Controls;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for MinionCardControl.xaml
    /// </summary>
    public partial class MinionCardControl : UserControl
    {
        public MinionCardControl()
        {
            InitializeComponent();
        }
        #region Properties

        public MinionDeckDesignModel CardDesign
        {
            get => (MinionDeckDesignModel)GetValue(CardDesignProperty);
            set => SetValue(CardDesignProperty, value);
        }

        public static readonly DependencyProperty CardDesignProperty =
            DependencyProperty.Register(nameof(CardDesign), typeof(MinionDeckDesignModel), typeof(MinionCardControl), new PropertyMetadata(new MinionDeckDesignModel()));

        public bool IsEditable
        {
            get => (bool)GetValue(IsEditableProperty);
            set => SetValue(IsEditableProperty, value);
        }

        public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.Register(nameof(IsEditable), typeof(bool), typeof(MinionCardControl), new PropertyMetadata(false));

        public MinionCardModel MinionCard
        {
            get => (MinionCardModel)GetValue(MinionCardProperty);
            set => SetValue(MinionCardProperty, value);
        }

        public static readonly DependencyProperty MinionCardProperty =
            DependencyProperty.Register(nameof(MinionCard), typeof(MinionCardModel), typeof(MinionCardControl), new PropertyMetadata(null));

        public MinionModel Minion
        {
            get => (MinionModel)GetValue(MinionProperty);
            set => SetValue(MinionProperty, value);
        }

        public static readonly DependencyProperty MinionProperty =
            DependencyProperty.Register(nameof(Minion), typeof(MinionModel), typeof(MinionCardControl), new PropertyMetadata(null));

        #endregion
    }
}
