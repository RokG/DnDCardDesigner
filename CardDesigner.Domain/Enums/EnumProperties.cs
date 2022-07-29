namespace CardDesigner.Domain.Enums
{

    public static class EnumProperties
    {
        public static EnumPropertyObject GetEnumProperties(object enumObject)
        {
            if (enumObject is RangeType rangeType)
            {
                switch (rangeType)
                {
                    case RangeType.Self:
                        return new(rangeType.ToString(), string.Empty, 0, 0, false);
                    case RangeType.Touch:
                        return new(rangeType.ToString(), string.Empty, 0, 0, false);
                    case RangeType.Distance:
                        return new(rangeType.ToString(), "ft", 0, 100, true);
                    default:
                        return new(string.Empty, string.Empty, -1, -1, false);
                }
            }
            else
            {
                return null;
            }
        }

        //public static PropertyObject GetEnumProperties(AreaOfEffect areaOfEffect)
        //{
        //    switch (areaOfEffect)
        //    {
        //        case AreaOfEffect.Sphere:
        //            break;
        //        case AreaOfEffect.Square:
        //            break;
        //        case AreaOfEffect.Cone:
        //            break;
        //        case AreaOfEffect.Line:
        //            break;
        //    }
        //}

    }
}
