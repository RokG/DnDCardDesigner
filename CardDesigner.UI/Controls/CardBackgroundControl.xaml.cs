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
    /// Interaction logic for CardBackgroundControl.xaml
    /// </summary>
    public partial class CardBackgroundControl : UserControl
    {
        public CardBackgroundControl()
        {
            InitializeComponent();
        }

        public CardDesignModel  CardDesign
        {
            get { return (CardDesignModel)GetValue(CardDesignProperty); }
            set { SetValue(CardDesignProperty, value); }
        }

        public static readonly DependencyProperty CardDesignProperty =
            DependencyProperty.Register(nameof(CardDesign), typeof(CardDesignModel), typeof(CardBackgroundControl), new PropertyMetadata(new CardDesignModel()));

    }
}
