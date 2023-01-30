using CardDesigner.Domain.Interfaces;
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
                if (addEditItem.SelectedItem is ICardDesign cardDesign)
                {
                    switch (cardDesign)
                    {
                        case SpellDeckDesignModel spellDeckDesign:
                            // Foregrounds
                            SetStartingColors(FrontLineColor, spellDeckDesign.FrontLineColor);
                            SetStartingColors(FrontBackgroundColor, spellDeckDesign.FrontBackgroundColor);
                            SetStartingColors(FrontFooterColor, spellDeckDesign.FrontFooterColor);
                            SetStartingColors(FrontHeaderTextColor, spellDeckDesign.FrontHeaderTextColor);
                            SetStartingColors(FrontFooterTextColor, spellDeckDesign.FrontFooterTextColor);
                            SetStartingColors(FrontDescriptionTextColor, spellDeckDesign.FrontDescriptionTextColor);
                            SetStartingColors(FrontHeaderIconColor, spellDeckDesign.FrontHeaderIconColor);
                            SetStartingColors(FrontFooterIconColor, spellDeckDesign.FrontFooterIconColor);
                            SetStartingColors(FrontHeaderColor, spellDeckDesign.FrontHeaderColor);
                            SetStartingColors(FrontHiglightColor, spellDeckDesign.FrontHiglightColor);
                            SetStartingColors(FrontForegroundColor, spellDeckDesign.FrontForegroundColor);
                            if (spellDeckDesign.FrontFooterIconColor != null)
                            {
                                SolidColorBrush solidColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(spellDeckDesign.FrontFooterIconColor));
                                Application.Current.Resources["IconColorFooter"] = solidColorBrush;
                            }

                            if (spellDeckDesign.FrontHeaderIconColor != null)
                            {
                                SolidColorBrush solidColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(spellDeckDesign.FrontHeaderIconColor));
                                Application.Current.Resources["IconColorHeader"] = solidColorBrush;
                            }
                            break;
                        case CharacterDeckDesignModel characterDeckDesign:
                            // Backgrounds
                            SetStartingColors(BackLineColor, characterDeckDesign.BackLineColor);
                            SetStartingColors(BackBackgroundColor, characterDeckDesign.BackBackgroundColor);
                            break;
                        default:
                            break;
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
