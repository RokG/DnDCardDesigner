using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for RepeatingControl.xaml
    /// </summary>
    public partial class RepeatingControl : UserControl
    {
        public RepeatingControl()
        {
            InitializeComponent();
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(RepeatingControl), new PropertyMetadata(string.Empty));

        public List<int> RepatableItems
        {
            get => (List<int>)GetValue(RepatableItemsProperty);
            set => SetValue(RepatableItemsProperty, value);
        }

        public static readonly DependencyProperty RepatableItemsProperty =
            DependencyProperty.Register(nameof(RepatableItems), typeof(List<int>), typeof(RepeatingControl), new PropertyMetadata(null));

        public int RepeatAmount
        {
            get => (int)GetValue(RepeatAmountProperty);
            set => SetValue(RepeatAmountProperty, value);
        }

        public static readonly DependencyProperty RepeatAmountProperty =
            DependencyProperty.Register(nameof(RepeatAmount), typeof(int), typeof(RepeatingControl), new PropertyMetadata(0, GenerateItems));

        private static void GenerateItems(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RepeatingControl repeatingControl)
            {
                List<int> items = new();

                for (int i = 0; i < repeatingControl.RepeatAmount; i++)
                {
                    items.Add(i);
                }

                repeatingControl.RepatableItems = items;
            }
        }
    }
}
