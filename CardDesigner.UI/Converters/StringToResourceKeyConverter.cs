using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace CardDesigner.UI.Converters
{
    public class StringToResourceKeyConverter : MarkupExtension, IValueConverter
    {
        public string Formatter { get; set; } = string.Empty;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string resourceKey;

            if (Formatter != string.Empty)
            {
                resourceKey = Formatter.Replace("(I)", value.ToString());
            }
            else
            {
                resourceKey = value.ToString();
            }

            return Application.Current.Resources[resourceKey];

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
