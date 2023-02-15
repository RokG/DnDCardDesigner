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

        public DeckBackgroundDesignModel CardDesign
        {
            get => (DeckBackgroundDesignModel)GetValue(CardDesignProperty);
            set => SetValue(CardDesignProperty, value);
        }

        public static readonly DependencyProperty CardDesignProperty =
            DependencyProperty.Register(nameof(CardDesign), typeof(DeckBackgroundDesignModel), typeof(CardBackgroundControl), new PropertyMetadata(new DeckBackgroundDesignModel()));

    }
}
