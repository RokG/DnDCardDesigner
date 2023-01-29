using System;
using System.Threading.Tasks.Dataflow;
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

        public double Saturation
        {
            get { return (double)GetValue(SVXProperty); }
            set { SetValue(SVXProperty, value); }
        }

        public static readonly DependencyProperty SVXProperty =
            DependencyProperty.Register(nameof(Saturation), typeof(double), typeof(ColorPickerControl), new PropertyMetadata(50.0));

        public double Value
        {
            get { return (double)GetValue(SVYProperty); }
            set { SetValue(SVYProperty, value); }
        }

        public static readonly DependencyProperty SVYProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(ColorPickerControl), new PropertyMetadata(50.0));

        public double Hue
        {
            get { return (double)GetValue(HXProperty); }
            set { SetValue(HXProperty, value); }
        }

        public static readonly DependencyProperty HXProperty =
            DependencyProperty.Register(nameof(Hue), typeof(double), typeof(ColorPickerControl), new PropertyMetadata(50.0));

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
            DependencyProperty.Register(nameof(CurrentColor), typeof(string), typeof(ColorPickerControl), new PropertyMetadata("#ff0000"));

        private string GetColorFromRectangle(Rectangle rectangle, double X, double Y)
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
            int x = Math.Clamp((int)X, 0, width - 1);
            int y = Math.Clamp((int)Y, 0, height - 1);

            // GET RGBA from pixel values (format is in BGRA)
            byte r = pixels[(x + width * y) * 4 + 2];
            byte g = pixels[(x + width * y) * 4 + 1];
            byte b = pixels[(x + width * y) * 4 + 0];
            byte a = pixels[(x + width * y) * 4 + 3];

            // Finaly output color
            return Color.FromArgb(a, r, g, b).ToString();
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

                if (ColorChanged != null)
                {
                    ColorChanged(this, new RoutedEventArgs());
                }
            }
        }

        private static BitmapSource CreateBitmapSourceFromVisual(
        double width,
        double height,
        Visual visualToRender,
        bool undoTransformation)
        {
            if (visualToRender == null)
            {
                return null;
            }
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)Math.Ceiling(width),
                (int)Math.Ceiling(height), 96, 96, PixelFormats.Pbgra32);

            if (undoTransformation)
            {
                DrawingVisual dv = new DrawingVisual();
                using (DrawingContext dc = dv.RenderOpen())
                {
                    VisualBrush vb = new VisualBrush(visualToRender);
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
    }
}
