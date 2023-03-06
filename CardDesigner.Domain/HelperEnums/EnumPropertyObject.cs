namespace CardDesigner.Domain.HelperEnums
{
    public class EnumPropertyObject
    {
        public EnumPropertyObject(
            string unit,
            double minimum,
            double maximum,
            bool hasSetValue)
        {
            Unit = unit;
            Minimum = minimum;
            Maximum = maximum;
            HasSetValue = hasSetValue;
        }

        public string Unit { get; }
        public double Minimum { get; }
        public double Maximum { get; }
        public bool HasSetValue { get; }
    }
}
