using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
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
    /// Interaction logic for CardPageControl.xaml
    /// </summary>
    public partial class CardPageControl : UserControl
    {
        public CardPageControl()
        {
            InitializeComponent();
        }

        public CharacterModel Character
        {
            get { return (CharacterModel)GetValue(CharacterProperty); }
            set { SetValue(CharacterProperty, value); }
        }

        public static readonly DependencyProperty CharacterProperty =
            DependencyProperty.Register(nameof(Character), typeof(CharacterModel), typeof(CardPageControl), new PropertyMetadata(new CharacterModel()));

        public ICardDesign CardDesign
        {
            get { return (ICardDesign)GetValue(CardDesignProperty); }
            set { SetValue(CardDesignProperty, value); }
        }

        public static readonly DependencyProperty CardDesignProperty =
            DependencyProperty.Register(nameof(CardDesign), typeof(ICardDesign), typeof(CardPageControl), new PropertyMetadata(null));

        public IEnumerable<ICard> Cards
        {
            get { return (IEnumerable<ICard>)GetValue(CardsProperty); }
            set { SetValue(CardsProperty, value); }
        }

        public static readonly DependencyProperty CardsProperty =
            DependencyProperty.Register(nameof(Cards), typeof(IEnumerable<ICard>), typeof(CardPageControl), new PropertyMetadata(null));

        public double CardSize
        {
            get { return (double)GetValue(CardSizeProperty); }
            set { SetValue(CardSizeProperty, value); }
        }

        public static readonly DependencyProperty CardSizeProperty =
            DependencyProperty.Register(nameof(CardSize), typeof(double), typeof(CardPageControl), new PropertyMetadata(635.0));
    }
}
