using CardDesigner.Domain.Enums;

namespace CardDesigner.Domain.HelperEnums
{
    public static class EnumProperties
    {
        public static EnumPropertyObject GetEnumProperties(object enumObject)
        {
            if (enumObject is RangeType rangeType)
            {
                return rangeType switch
                {
                    RangeType.Self => new("Self", 0, 0, false),
                    RangeType.Touch => new("Tch", 0, 0, false),
                    RangeType.Distance => new("ft", 0, 100, true),
                    _ => new(string.Empty, -1, -1, false),
                };
            }
            else if (enumObject is CastingTimeType castingTimeType)
            {
                return castingTimeType switch
                {
                    CastingTimeType.Action => new("A", 0, 0, false),
                    CastingTimeType.BonusAction => new("BA", 0, 0, false),
                    CastingTimeType.Reaction => new("R", 0, 0, false),
                    CastingTimeType.Second => new("sec", 0, 60, true),
                    CastingTimeType.Minute => new("min", 0, 60, true),
                    CastingTimeType.Hour => new("h", 0, 24, true),
                    CastingTimeType.Day => new("day", 0, 7, true),
                    _ => new(string.Empty, -1, -1, false),
                };
            }
            else if (enumObject is DurationType durationType)
            {
                return durationType switch
                {
                    DurationType.Instantaneous => new("Ins.", 0, 0, false),
                    DurationType.Second => new("sec", 1, 60, true),
                    DurationType.Minute => new("min", 1, 60, true),
                    DurationType.Hour => new("h", 1, 24, true),
                    DurationType.Day => new("day", 1, 7, true),
                    _ => new(string.Empty, -1, -1, false),
                };
            }
            else if (enumObject is AreaOfEffect areaOfEffect)
            {
                return areaOfEffect switch
                {
                    AreaOfEffect.None => new("", 0, 100, false),
                    AreaOfEffect.Sphere => new("ft", 0, 100, true),
                    AreaOfEffect.Square => new("ft", 0, 100, true),
                    AreaOfEffect.Cone => new("ft", 0, 100, true),
                    AreaOfEffect.Line => new("ft", 0, 100, true),
                    _ => new(string.Empty, -1, -1, false),
                };
            }
            else if (enumObject is MagicDamageType)
            {
                return new(string.Empty, 0, 10, false);
            }
            else if (enumObject is DiceType diceType)
            {
                return diceType switch
                {
                    DiceType.None => new(string.Empty, 0, 0, false),
                    DiceType.d4 => new(string.Empty, 1, 10, true),
                    DiceType.d6 => new(string.Empty, 1, 10, true),
                    DiceType.d8 => new(string.Empty, 1, 10, true),
                    DiceType.d10 => new(string.Empty, 1, 10, true),
                    DiceType.d12 => new(string.Empty, 1, 10, true),
                    DiceType.d20 => new(string.Empty, 1, 10, false),
                    DiceType.d100 => new(string.Empty, 1, 10, false),
                    _ => new(string.Empty, -1, -1, false),
                };
            }
            else if (enumObject is TargetType targetType)
            {
                return targetType switch
                {
                    TargetType.Self => new("N/A", 0, 0, false),
                    TargetType.Touch => new("N/A", 0, 0, false),
                    TargetType.Target => new("ft", 0, 1000, true),
                    _ => new(string.Empty, -1, -1, false),
                };
            }
            else
            {
                return new(string.Empty, -1, -1, false);
            }
        }

    }
}
