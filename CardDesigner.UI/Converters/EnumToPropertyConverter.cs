using CardDesigner.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace CardDesigner.UI.Converters
{
    public class EnumToPropertyConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EnumPropertyObject props = EnumProperties.GetEnumProperties(value);
            if (props != null && parameter != null)
            {
                switch (parameter)
                {
                    case nameof(EnumPropertyTypes.Minimum):
                        return props.Minimum;
                    case nameof(EnumPropertyTypes.Maximum):
                        return props.Maximum;
                    case nameof(EnumPropertyTypes.Unit):
                        return props.Unit;
                    case nameof(EnumPropertyTypes.HasSetValue):
                        return props.HasSetValue;
                    default:
                        return Binding.DoNothing;
                }
            }
            else return Binding.DoNothing;
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
