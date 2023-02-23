using CardDesigner.Domain.Enums;

namespace CardDesigner.Domain.HelperModels
{
    public class UsableModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Uses { get; set; }
        public CastingTimeType UseTimeType { get; set; }
        public int UseTimeValue { get; set; }
    }
}
