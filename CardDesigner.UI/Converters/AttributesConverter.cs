using CardDesigner.Domain.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CardDesigner.UI.Converters
{
    public class AttributesConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CharacterAttributesModel attributesModel && parameter is CardDesigner.Domain.Enums.Attribute attribute)
            {
                return attributesModel.GetAttribute(attribute);
            }
            else
            {
                return new CharacterAttributesModel();
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
