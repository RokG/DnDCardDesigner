using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CardDesigner.UI.Controls
{
    /// <summary>
    /// Interaction logic for ColorPickerControl.xaml
    /// </summary>
    public partial class ColorPickerControl : UserControl
    {

        public ColorPickerControl()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler ColorChanged;

        #region Properties

        public double Saturation
        {
            get => (double)GetValue(SVXProperty);
            set => SetValue(SVXProperty, value);
        }

        public static readonly DependencyProperty SVXProperty =
            DependencyProperty.Register(nameof(Saturation), typeof(double), typeof(ColorPickerControl), new PropertyMetadata(50.0));

        public double Value
        {
            get => (double)GetValue(SVYProperty);
            set => SetValue(SVYProperty, value);
        }

        public static readonly DependencyProperty SVYProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(ColorPickerControl), new PropertyMetadata(50.0));

        public double Hue
        {
            get => (double)GetValue(HXProperty);
            set => SetValue(HXProperty, value);
        }

        public static readonly DependencyProperty HXProperty =
            DependencyProperty.Register(nameof(Hue), typeof(double), typeof(ColorPickerControl), new PropertyMetadata(50.0, SetHue));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(ColorPickerControl), new PropertyMetadata(string.Empty));

        public string CurrentHueValue
        {
            get => (string)GetValue(CurrentHueValueProperty);
            set => SetValue(CurrentHueValueProperty, value);
        }

        public static readonly DependencyProperty CurrentHueValueProperty =
            DependencyProperty.Register(nameof(CurrentHueValue), typeof(string), typeof(ColorPickerControl), new PropertyMetadata("#ffFF0000"));

        public string CurrentColor
        {
            get => (string)GetValue(CurrentColorProperty);
            set => SetValue(CurrentColorProperty, value);
        }

        public static readonly DependencyProperty CurrentColorProperty =
            DependencyProperty.Register(nameof(CurrentColor), typeof(string), typeof(ColorPickerControl), new PropertyMetadata("#ff0000", SetCurrentColor));

        #endregion

        #region Methods

        private static void SetHue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorPickerControl colorPicker && e.NewValue is double hueValue)
            {
                colorPicker.CurrentHueValue = GetColorFromRectangle(colorPicker.hueRectangle, 0, hueValue);
            }
        }

        private static void SetCurrentColor(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorPickerControl colorPicker && e.NewValue is string color)
            {
                if (color.Length == 9)
                {
                    color = "#" + color[3..].PadRight(6);
                }
                if (color.Length < 7)
                {
                    color = color.PadRight(7, '0');
                }
                colorPicker.CurrentColor = color;
                ColorHSV colorHSV = GetHSV(color);
                colorPicker.Hue = colorHSV.Hue;
                colorPicker.Saturation = colorHSV.Saturation;
                colorPicker.Value = colorHSV.Value;
            }
        }

        private static string GetColorFromRectangle(Rectangle rectangle, double X, double Y)
        {
            // Create imagesource from rectangle
            int width = (int)rectangle.Width;
            int height = (int)rectangle.Height;

            BitmapSource img = CreateBitmapSourceFromVisual(width, height, rectangle, true);

            int stride = img.PixelWidth * 4;
            int size = img.PixelHeight * stride;
            byte[] pixels = new byte[size];
            img.CopyPixels(pixels, stride, 0);

            // Clamp values
            int x = Math.Clamp((int)Math.Round(X), 0, width - 1);
            int y = Math.Clamp((int)Math.Round(Y), 0, height - 1);

            // GET RGBA from pixel values (format is in BGRA)
            byte a = pixels[(x + width * y) * 4 + 3];
            byte r = pixels[(x + width * y) * 4 + 2];
            byte g = pixels[(x + width * y) * 4 + 1];
            byte b = pixels[(x + width * y) * 4 + 0];

            // Finaly output color
            return Color.FromArgb(a, r, g, b).ToString();
        }
        private struct ColorHSV
        {
            public double Hue, Saturation, Value;
        }
        private static ColorHSV GetHSV(string colorHexRGB)
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

        private void Rectangle_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Rectangle rectangle)
            {
                // Get mouse coordinate within rectangle
                Point colorCoordinate;
                if (rectangle.Name == levelSaturationRectangle.Name)
                {
                    colorCoordinate = e.GetPosition(levelSaturationRectangle);
                }
                else if (rectangle.Name == hueRectangle.Name)
                {
                    colorCoordinate = e.GetPosition(hueRectangle);
                }

                // Output the color
                if (rectangle.Name == levelSaturationRectangle.Name)
                {
                    Saturation = colorCoordinate.X;
                    Value = colorCoordinate.Y;
                    CurrentColor = GetColorFromRectangle(rectangle, Saturation, Value);
                }
                else if (rectangle.Name == hueRectangle.Name)
                {
                    Hue = colorCoordinate.Y;
                    CurrentHueValue = GetColorFromRectangle(rectangle, 0, Hue);
                    CurrentColor = GetColorFromRectangle(levelSaturationRectangle, Saturation, Value);
                }

                ColorChanged?.Invoke(this, new RoutedEventArgs());
            }
        }

        private static BitmapSource CreateBitmapSourceFromVisual(double width, double height, Visual visualToRender, bool undoTransformation)
        {
            if (visualToRender == null)
            {
                return null;
            }
            RenderTargetBitmap bmp = new((int)Math.Ceiling(width),
                (int)Math.Ceiling(height), 96, 96, PixelFormats.Pbgra32);

            if (undoTransformation)
            {
                DrawingVisual dv = new();
                using (DrawingContext dc = dv.RenderOpen())
                {
                    VisualBrush vb = new(visualToRender);
                    dc.DrawRectangle(vb, null, new Rect(new Point(), new Size(width, height)));
                }
                bmp.Render(dv);
            }
            else
            {
                bmp.Render(visualToRender);
            }
            return bmp;
        }

        #endregion

        private void TextBoxColor_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (e.Key == Key.Enter)
                {
                    tb.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
            }
        }
    }
}
