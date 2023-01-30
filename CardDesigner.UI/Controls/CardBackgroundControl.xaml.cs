using CardDesigner.Domain.Models;
using System.Windows;
using System.Windows.Controls;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for CardBackgroundControl.xaml
    /// </summary>
    public partial class CardBackgroundControl : UserControl
    {
        public CardBackgroundControl()
        {
            InitializeComponent();
        }

        public CharacterDeckDesignModel CardDesign
        {
            get => (CharacterDeckDesignModel)GetValue(CardDesignProperty);
            set => SetValue(CardDesignProperty, value);
        }

        public static readonly DependencyProperty CardDesignProperty =
            DependencyProperty.Register(nameof(CardDesign), typeof(CharacterDeckDesignModel), typeof(CardBackgroundControl), new PropertyMetadata(new CharacterDeckDesignModel()));

    }
}
