using CardDesigner.Domain.Models;
using System.Windows;
using System.Windows.Controls;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for CharacterCardControl.xaml
    /// </summary>
    public partial class CharacterCardControl : UserControl
    {
        public CharacterCardControl()
        {
            InitializeComponent();
        }

        #region Properties

        public DeckBackgroundDesignModel CardDesign
        {
            get => (DeckBackgroundDesignModel)GetValue(CardDesignProperty);
            set => SetValue(CardDesignProperty, value);
        }

        public static readonly DependencyProperty CardDesignProperty =
            DependencyProperty.Register(nameof(CardDesign), typeof(DeckBackgroundDesignModel), typeof(CharacterCardControl), new PropertyMetadata(new DeckBackgroundDesignModel()));

        public bool IsEditable
        {
            get => (bool)GetValue(IsEditableProperty);
            set => SetValue(IsEditableProperty, value);
        }

        public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.Register(nameof(IsEditable), typeof(bool), typeof(CharacterCardControl), new PropertyMetadata(false));

        public CharacterCardModel CharacterCard
        {
            get => (CharacterCardModel)GetValue(CharacterCardProperty);
            set => SetValue(CharacterCardProperty, value);
        }

        public static readonly DependencyProperty CharacterCardProperty =
            DependencyProperty.Register(nameof(CharacterCard), typeof(CharacterCardModel), typeof(CharacterCardControl), new PropertyMetadata(null));

        public CharacterModel Character
        {
            get { return (CharacterModel)GetValue(CharacterProperty); }
            set { SetValue(CharacterProperty, value); }
        }

        public static readonly DependencyProperty CharacterProperty =
            DependencyProperty.Register(nameof(Character), typeof(CharacterModel), typeof(CharacterCardControl), new PropertyMetadata(null));

        #endregion
    }
}
