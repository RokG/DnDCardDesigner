using CardDesigner.Domain.Enums;

namespace CardDesigner.Domain.HelperModels
{
    public class ConsumableModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Doses { get; set; }
        public CastingTimeType UseTimeType { get; set; }
        public int UseTimeValue { get; set; }
    }
}
