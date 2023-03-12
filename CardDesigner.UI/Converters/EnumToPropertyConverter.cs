using CardDesigner.Domain.Enums;
using CardDesigner.Domain.HelperEnums;
using System;
using System.Globalization;
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
                return parameter switch
                {
                    nameof(EnumPropertyTypes.Minimum) => props.Minimum,
                    nameof(EnumPropertyTypes.Maximum) => props.Maximum,
                    nameof(EnumPropertyTypes.Unit) => props.Unit,
                    nameof(EnumPropertyTypes.HasSetValue) => props.HasSetValue,
                    _ => Binding.DoNothing,
                };
            }
            else
            {
                return Binding.DoNothing;
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
