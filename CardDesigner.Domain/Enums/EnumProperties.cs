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
                        return new("Self", 0, 0, false);
                    case RangeType.Touch:
                        return new("Tch", 0, 0, false);
                    case RangeType.Distance:
                        return new("ft", 0, 100, true);
                    default:
                        return new(string.Empty, -1, -1, false);
                }
            }
            else if (enumObject is CastingTimeType castingTimeType)
            {
                switch (castingTimeType)
                {
                    case CastingTimeType.Action:
                        return new("A", 0, 0, false);
                    case CastingTimeType.BonusAction:
                        return new("BA", 0, 0, false);
                    case CastingTimeType.Reaction:
                        return new("R", 0, 0, false);
                    case CastingTimeType.Second:
                        return new("sec", 0, 60, true);
                    case CastingTimeType.Minute:
                        return new("min", 0, 60, true);
                    case CastingTimeType.Hour:
                        return new("h", 0, 24, true);
                    case CastingTimeType.Day:
                        return new("day", 0, 7, true);
                    default:
                        return new(string.Empty, -1, -1, false);
                }
            }
            else if (enumObject is DurationType durationType)
            {
                switch (durationType)
                {
                    case DurationType.Instantaneous:
                        return new("Ins.", 0, 0, false);
                    case DurationType.Second:
                        return new("sec", 1, 60, true);
                    case DurationType.Minute:
                        return new("min", 1, 60, true);
                    case DurationType.Hour:
                        return new("h", 1, 24, true);
                    case DurationType.Day:
                        return new("day", 1, 7, true);
                    default:
                        return new(string.Empty, -1, -1, false);
                }
            }
            else if (enumObject is AreaOfEffect areaOfEffect)
            {
                return new("ft", 0, 100, true);
            }
            else if (enumObject is MagicDamageType damageType)
            {
                return new(string.Empty, 0, 10, false);
            }
            else if (enumObject is DiceType diceType)
            {
                if (diceType == DiceType.d100 || diceType == DiceType.d20)
                {
                return new(string.Empty, 1, 10, false);
                }
                else
                { 
                return new(string.Empty, 1, 10, true);
                }
            }
            else if (enumObject is TargetType targetType)
            {
                switch (targetType)
                {
                    case TargetType.Self:
                        return new("N/A", 0, 0, false);
                    case TargetType.Touch:
                        return new("N/A", 0, 0, false);
                    case TargetType.Target:
                        return new("ft", 0, 1000, true);
                    default:
                        return new(string.Empty, -1, -1, false);
                }
            }
            else
            {
                return new(string.Empty, -1, -1, false);
            }
        }

    }
}
