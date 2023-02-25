using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CardDesigner.UI.Converters
{
    public class PercentageConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double value1 = System.Convert.ToDouble(value);
            double value2 = System.Convert.ToDouble(parameter);

            return value1 * value2 / 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
