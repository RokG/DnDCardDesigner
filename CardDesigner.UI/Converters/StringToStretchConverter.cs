using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace CardDesigner.UI.Converters
{
    internal class StringToStretchConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stretch)
            {
                stretch = stretch.Replace(" ", string.Empty);
                return Stretch.TryParse(stretch, out Stretch stretchProperty) ? stretchProperty : Stretch.None;
            }
            else
            {
                return Stretch.None;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
