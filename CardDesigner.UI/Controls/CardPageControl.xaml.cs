using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
            get => (CharacterModel)GetValue(CharacterProperty);
            set => SetValue(CharacterProperty, value);
        }

        public static readonly DependencyProperty CharacterProperty =
            DependencyProperty.Register(nameof(Character), typeof(CharacterModel), typeof(CardPageControl), new PropertyMetadata(new CharacterModel()));

        public ICardDesign CardDesign
        {
            get => (ICardDesign)GetValue(CardDesignProperty);
            set => SetValue(CardDesignProperty, value);
        }

        public static readonly DependencyProperty CardDesignProperty =
            DependencyProperty.Register(nameof(CardDesign), typeof(ICardDesign), typeof(CardPageControl), new PropertyMetadata(null));

        public IEnumerable<ICard> Cards
        {
            get => (IEnumerable<ICard>)GetValue(CardsProperty);
            set => SetValue(CardsProperty, value);
        }

        public static readonly DependencyProperty CardsProperty =
            DependencyProperty.Register(nameof(Cards), typeof(IEnumerable<ICard>), typeof(CardPageControl), new PropertyMetadata(null));

        public double CardSize
        {
            get => (double)GetValue(CardSizeProperty);
            set => SetValue(CardSizeProperty, value);
        }

        public static readonly DependencyProperty CardSizeProperty =
            DependencyProperty.Register(nameof(CardSize), typeof(double), typeof(CardPageControl), new PropertyMetadata(635.0));

        public double PageOffsetX
        {
            get => (double)GetValue(PageOffsetXProperty);
            set => SetValue(PageOffsetXProperty, value);
        }

        public static readonly DependencyProperty PageOffsetXProperty =
            DependencyProperty.Register(nameof(PageOffsetX), typeof(double), typeof(CardPageControl), new PropertyMetadata(0.0, PageOffsetChange));

        public Thickness PageOffset
        {
            get => (Thickness)GetValue(PageOffsetProperty);
            set => SetValue(PageOffsetProperty, value);
        }

        public static readonly DependencyProperty PageOffsetProperty =
            DependencyProperty.Register(nameof(PageOffset), typeof(Thickness), typeof(CardPageControl), new PropertyMetadata(new Thickness()));

        private static void PageOffsetChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CardPageControl pageControl)
            {
                pageControl.PageOffset = new Thickness(pageControl.PageOffsetX, 0, 0, 0);
            }
        }
    }
}
