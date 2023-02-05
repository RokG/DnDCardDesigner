using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CardDesigner.UI.Converters
{
    public class TypeToStringConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "null";
            }
            else
            {
                return value.GetType().Name;
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

    /// This converter is needed because these two things are not working:

    /// This is throwing binding failures when switching type, although everything works
    //<ContentControl>
    //    <ContentControl.Style>
    //        <Style TargetType = "{x:Type ContentControl}" >
    //            < Style.Triggers >
    //                < DataTrigger
    //                    Binding="{Binding ItemCard.Type, ElementName=itemCard, Mode=OneWay}"
    //                    Value="{x:Static enums:ItemType.Armour}">
    //                    <Setter Property = "Template" Value="{StaticResource ArmorTemplate}" />
    //                </DataTrigger>
    //                <DataTrigger
    //                    Binding = "{Binding ItemCard.Type, ElementName=itemCard, Mode=OneWay}"
    //                    Value="{x:Static enums:ItemType.Weapon}">
    //                    <Setter Property = "Template" Value="{StaticResource WeaponTemplate}" />
    //                </DataTrigger>
    //            </Style.Triggers>
    //        </Style>
    //    </ContentControl.Style>
    //</ContentControl>

    /// This is not getting triggered, altho the binding seems to be Class and not INterface when checking with converter
    // <ContentControl Margin = "5,0" >
    //    < ContentControl.Style >
    //        < Style TargetType="{x:Type ContentControl}">
    //            <Style.Triggers>
    //                <DataTrigger
    //                    Binding = "{Binding SelectedItemCard.Item}"
    //                    Value="{x:Type models:ArmourModel}">
    //                    <Setter Property = "Template" Value="{StaticResource ArmorTemplate}" />
    //                </DataTrigger>
    //                <DataTrigger
    //                    Binding = "{Binding SelectedItemCard.Item}"
    //                    Value="{x:Type models:WeaponModel}">
    //                    <Setter Property = "Template" Value="{StaticResource WeaponTemplate}" />
    //                </DataTrigger>
    //            </Style.Triggers>
    //        </Style>
    //    </ContentControl.Style>
    //</ContentControl>
}
