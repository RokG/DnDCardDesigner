using CardDesigner.Domain.Models;
using CardDesigner.UI.Controls;
using System;
using System.Globalization;
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

        private struct ColorHSV
        {
            public double Hue, Saturation, Value;
        }

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
                    SetStartingColors(FrontLineColor, cardDesign.FrontLineColor);
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
            ColorHSV color = GetHSV(hexValue);
            ColorHSV color2 = GetHSL(hexValue);

            colorPickerControl.Hue = color.Hue;
            colorPickerControl.Saturation = color.Saturation;
            colorPickerControl.Value = color.Value;

            //colorPickerControl.CurrentHueValue = hexValue;
        }

        private ColorHSV GetHSV(string colorHexRGB)
        {
            //https://www.codeproject.com/Questions/996265/RGB-to-HSV-conversion
            int argb = int.Parse(colorHexRGB.Replace("#", ""), NumberStyles.HexNumber);
            System.Drawing.Color colorRGB = System.Drawing.Color.FromArgb(argb);

            double max = Math.Max(colorRGB.R, Math.Max(colorRGB.G, colorRGB.B));
            double min = Math.Min(colorRGB.R, Math.Min(colorRGB.G, colorRGB.B));

            ColorHSV colorHSV = new()
            {
                Hue = Math.Round(colorRGB.GetHue() * 100 / 360, 3),
                Saturation = Math.Round(((max == 0) ? 0 : 1d - (1d * min / max)) * 100, 3),
                Value = Math.Round(100 - ((max / 255d) * 100), 3)
            };

            return colorHSV;
        }

        private ColorHSV GetHSL(string colorHexRGB)
        {
            //https://www.codeproject.com/Questions/996265/RGB-to-HSV-conversion
            int argb = int.Parse(colorHexRGB.Replace("#", ""), NumberStyles.HexNumber);
            System.Drawing.Color colorRGB = System.Drawing.Color.FromArgb(argb);

            ColorHSV colorHSV = new()
            {
                Hue = colorRGB.GetHue() * 100 / 360,
                Saturation = colorRGB.GetSaturation() * 100,
                Value = colorRGB.GetBrightness() * 100,
            };

            return colorHSV;
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
