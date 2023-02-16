using CardDesigner.Domain.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CardDesigner.UI.Converters
{
    public class AbilitiesConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CharacterAbilitiesModel attributesModel && parameter is CardDesigner.Domain.Enums.Ability attribute)
            {
                return attributesModel.GetAbility(attribute);
            }
            else
            {
                return new CharacterAbilitiesModel();
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
