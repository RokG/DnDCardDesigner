namespace CardDesigner.Domain.Enums
{
    public class EnumPropertyObject
    {

        public EnumPropertyObject(
            string name,
            string unit,
            double minimum,
            double maximum,
            bool hasSetValue)
        {
            Name = name;
            Unit = unit;
            Minimum = minimum;
            Maximum = maximum;
            HasSetValue = hasSetValue;
        }

        public string Name { get; }
        public string Unit { get; }
        public double Minimum { get; }
        public double Maximum { get; }
        public bool HasSetValue { get; }
    }
}
