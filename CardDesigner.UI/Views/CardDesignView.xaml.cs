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
    /// Interaction logic for DeckDesignView.xaml
    /// </summary>
    public partial class DeckDesignView : UserControl
    {
        private struct ColorHSV
        {
            public double Hue, Saturation, Value;
        }

        public DeckDesignView()
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
                            SetStartingColors(FrontSpellLineColor, spellDeckDesign.LineColor);
                            SetStartingColors(FrontSpellBackgroundColor, spellDeckDesign.BackgroundColor);
                            SetStartingColors(FrontSpellFooterColor, spellDeckDesign.FooterColor);
                            SetStartingColors(FrontSpellHeaderTextColor, spellDeckDesign.HeaderTextColor);
                            SetStartingColors(FrontSpellFooterTextColor, spellDeckDesign.FooterTextColor);
                            SetStartingColors(FrontSpellDescriptionTextColor, spellDeckDesign.DescriptionTextColor);
                            SetStartingColors(FrontSpellHeaderIconColor, spellDeckDesign.HeaderIconColor);
                            SetStartingColors(FrontSpellFooterIconColor, spellDeckDesign.FooterIconColor);
                            SetStartingColors(FrontSpellHeaderColor, spellDeckDesign.HeaderColor);
                            break;
                        case ItemDeckDesignModel itemDeckDesign:
                            // Foregrounds
                            SetStartingColors(FrontItemLineColor, itemDeckDesign.LineColor);
                            SetStartingColors(FrontItemBackgroundColor, itemDeckDesign.BackgroundColor);
                            SetStartingColors(FrontItemFooterColor, itemDeckDesign.FooterColor);
                            SetStartingColors(FrontItemHeaderTextColor, itemDeckDesign.HeaderTextColor);
                            SetStartingColors(FrontItemFooterTextColor, itemDeckDesign.FooterTextColor);
                            SetStartingColors(FrontItemDescriptionTextColor, itemDeckDesign.DescriptionTextColor);
                            SetStartingColors(FrontItemHeaderIconColor, itemDeckDesign.HeaderIconColor);
                            SetStartingColors(FrontItemFooterIconColor, itemDeckDesign.FooterIconColor);
                            SetStartingColors(FrontItemHeaderColor, itemDeckDesign.HeaderColor);
                            break;
                        case CharacterDeckDesignModel characterDeckDesign:
                            // Backgrounds
                            SetStartingColors(BackLineColor, characterDeckDesign.LineColor);
                            SetStartingColors(BackBackgroundColor, characterDeckDesign.BackgroundColor);
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
    }
}
