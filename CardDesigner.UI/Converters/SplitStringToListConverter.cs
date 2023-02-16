using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CardDesigner.UI.Converters
{
    public class SplitStringToListConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string listOfItems)
            {
                return listOfItems.Split(",");
            }

            return Binding.DoNothing;
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
