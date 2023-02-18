using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
using CardDesigner.UI.Controls;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

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
                            SetSpellCardColors(spellDeckDesign);
                            break;
                        case CharacterDeckDesignModel characterDeckDesign:
                            SetCharacterCardColors(characterDeckDesign);
                            break;
                        case ItemDeckDesignModel itemDeckDesign:
                            SetItemCardColors(itemDeckDesign);
                            break;
                        case DeckBackgroundDesignModel characterDeckDesign:
                            SetBackgroundColors(characterDeckDesign);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void SetBackgroundColors(DeckBackgroundDesignModel characterDeckDesign)
        {
            SetStartingColors(BackLineColor, characterDeckDesign.LineColor);
            SetStartingColors(BackBackgroundColor, characterDeckDesign.BackgroundColor);
        }

        private void SetItemCardColors(ItemDeckDesignModel itemDeckDesign)
        {
            SetStartingColors(FrontItemLineColor, itemDeckDesign.LineColor);
            SetStartingColors(FrontItemBackgroundColor, itemDeckDesign.BackgroundColor);
            SetStartingColors(FrontItemFooterColor, itemDeckDesign.FooterColor);
            SetStartingColors(FrontItemHeaderTextColor, itemDeckDesign.HeaderTextColor);
            SetStartingColors(FrontItemFooterTextColor, itemDeckDesign.FooterTextColor);
            SetStartingColors(FrontItemDescriptionTextColor, itemDeckDesign.DescriptionTextColor);
            SetStartingColors(FrontItemHeaderIconColor, itemDeckDesign.HeaderIconColor);
            SetStartingColors(FrontItemFooterIconColor, itemDeckDesign.FooterIconColor);
            SetStartingColors(FrontItemHeaderColor, itemDeckDesign.HeaderColor);
        }

        private void SetSpellCardColors(SpellDeckDesignModel spellDeckDesign)
        {
            SetStartingColors(FrontSpellLineColor, spellDeckDesign.LineColor);
            SetStartingColors(FrontSpellBackgroundColor, spellDeckDesign.BackgroundColor);
            SetStartingColors(FrontSpellFooterColor, spellDeckDesign.FooterColor);
            SetStartingColors(FrontSpellHeaderTextColor, spellDeckDesign.HeaderTextColor);
            SetStartingColors(FrontSpellFooterTextColor, spellDeckDesign.FooterTextColor);
            SetStartingColors(FrontSpellDescriptionTextColor, spellDeckDesign.DescriptionTextColor);
            SetStartingColors(FrontSpellHeaderIconColor, spellDeckDesign.HeaderIconColor);
            SetStartingColors(FrontSpellFooterIconColor, spellDeckDesign.FooterIconColor);
            SetStartingColors(FrontSpellHeaderColor, spellDeckDesign.HeaderColor);
        }

        private void SetCharacterCardColors(CharacterDeckDesignModel characterDeckDesign)
        {
            SetStartingColors(FrontCharacterLineColor, characterDeckDesign.LineColor);
            SetStartingColors(FrontCharacterBackgroundColor, characterDeckDesign.BackgroundColor);
            SetStartingColors(FrontCharacterFooterColor, characterDeckDesign.FooterColor);
            SetStartingColors(FrontCharacterHeaderTextColor, characterDeckDesign.HeaderTextColor);
            SetStartingColors(FrontCharacterFooterTextColor, characterDeckDesign.FooterTextColor);
            SetStartingColors(FrontCharacterDescriptionTextColor, characterDeckDesign.DescriptionTextColor);
            SetStartingColors(FrontCharacterHeaderIconColor, characterDeckDesign.HeaderIconColor);
            SetStartingColors(FrontCharacterFooterIconColor, characterDeckDesign.FooterIconColor);
            SetStartingColors(FrontCharacterHeaderColor, characterDeckDesign.HeaderColor);
        }

        private void SetStartingColors(ColorPickerControl colorPickerControl, string hexValue)
        {
            ColorHSV color = GetHSV(hexValue);

            colorPickerControl.Hue = color.Hue;
            colorPickerControl.Saturation = color.Saturation;
            colorPickerControl.Value = color.Value;

            colorPickerControl.CurrentHueValue = ColorFromHSV(color.Hue, 1, 1);
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

        public static string ColorFromHSV(double hue, double saturation, double value)
        {
            hue = hue * 360 / 100;
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            string v = Convert.ToInt32(value).ToString("X2");
            string p = Convert.ToInt32(value * (1 - saturation)).ToString("X2");
            string q = Convert.ToInt32(value * (1 - f * saturation)).ToString("X2");
            string t = Convert.ToInt32(value * (1 - (1 - f) * saturation)).ToString("X2");

            if (hi == 0)
            {
                return $"#FF" + v + t + p;
            }
            else if (hi == 1)
            {
                return $"#FF" + q + v + p;
            }
            else if (hi == 2)
            {
                return $"#FF" + p + v + t;
            }
            else if (hi == 3)
            {
                return $"#FF" + p + q + v;
            }
            else if (hi == 4)
            {
                return $"#FF" + t + p + v;
            }
            else
            {
                return $"#FF" + v + p + q;
            }
        }
    }
}
