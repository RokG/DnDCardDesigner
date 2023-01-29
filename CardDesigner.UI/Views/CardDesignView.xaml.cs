using CardDesigner.Domain.Models;
using CardDesigner.UI.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CardDesigner.UI.Views
{
    /// <summary>
    /// Interaction logic for CardDesignView.xaml
    /// </summary>
    public partial class CardDesignView : UserControl
    {
        public CardDesignView()
        {
            InitializeComponent();
        }

        private void SelectedCardDesign_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (sender is AddEditItem addEditItem)
            {
                if (addEditItem.SelectedItem is CardDesignModel cardDesign)
                {
                    // Backgrounds
                    SetStartingColors(BackLineColor, cardDesign.BackLineColor);
                    SetStartingColors(BackBackgroundColor, cardDesign.BackBackgroundColor);
                    // Foregrounds
                    SetStartingColors(BackLineColor, cardDesign.FrontLineColor);
                    SetStartingColors(FrontBackgroundColor, cardDesign.FrontBackgroundColor);
                    SetStartingColors(FrontFooterColor, cardDesign.FrontFooterColor);
                    SetStartingColors(FrontHeaderTextColor, cardDesign.FrontHeaderTextColor);
                    SetStartingColors(FrontFooterTextColor, cardDesign.FrontFooterTextColor);
                    SetStartingColors(FrontDescriptionTextColor, cardDesign.FrontDescriptionTextColor);
                    SetStartingColors(FrontHeaderIconColor, cardDesign.FrontHeaderIconColor);
                    SetStartingColors(FrontFooterIconColor, cardDesign.FrontFooterIconColor);
                    SetStartingColors(FrontHeaderColor, cardDesign.FrontHeaderColor);
                    SetStartingColors(FrontHiglightColor, cardDesign.FrontHiglightColor);
                    SetStartingColors(FrontForegroundColor, cardDesign.FrontForegroundColor);

                    if (cardDesign.FrontFooterIconColor != null)
                    {
                        SolidColorBrush solidColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(cardDesign.FrontFooterIconColor));
                        Application.Current.Resources["IconColorFooter"] = solidColorBrush;
                    }

                    if (cardDesign.FrontHeaderIconColor != null)
                    {
                        SolidColorBrush solidColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(cardDesign.FrontHeaderIconColor));
                        Application.Current.Resources["IconColorHeader"] = solidColorBrush;
                    }
                }
            }
        }

        private void SetStartingColors(ColorPickerControl colorPickerControl, string hexValue)
        {
            colorPickerControl.Hue = GetColorFromHex(hexValue, 1);
            colorPickerControl.Saturation = GetColorFromHex(hexValue, 2);
            colorPickerControl.Value = GetColorFromHex(hexValue, 3);
            colorPickerControl.CurrentHueValue = hexValue;
        }

        private int GetColorFromHex(string hexColor, int HSV)
        {
            if (hexColor == null)
            {
                return 0;
            }
            string pureHex = hexColor.Replace("#", string.Empty);
            string hex = "0";
            int startIdx = 0;
            if (pureHex.Length == 8)
            {
                startIdx = 2;
            }
            switch (HSV)
            {
                case 1:
                    hex = pureHex.Substring(startIdx + 0, 2);
                    break;
                case 2:
                    hex = pureHex.Substring(startIdx + 2, 2);
                    break;
                case 3:
                    hex = pureHex.Substring(startIdx + 4, 2);
                    break;
                default:
                    break;
            }

            int colorInt = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);

            return colorInt * 100 / 256;
        }

        private void FrontFooterIconColor_ColorChanged(object sender, RoutedEventArgs e)
        {
            if (sender is ColorPickerControl colorPicker)
            {
                SolidColorBrush solidColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorPicker.CurrentColor));
                Application.Current.Resources["IconColorFooter"] = solidColorBrush;
            }
        }

        private void FrontHeaderIconColor_ColorChanged(object sender, RoutedEventArgs e)
        {
            if (sender is ColorPickerControl colorPicker)
            {
                SolidColorBrush solidColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorPicker.CurrentColor));
                Application.Current.Resources["IconColorHeader"] = solidColorBrush;
            }
        }
    }
}
