using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;

namespace CardDesigner.Domain.HelperModels
{
    public class ConsumableModel : IItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Doses { get; set; }
        public CastingTimeType UseTimeType { get; set; }
        public int UseTimeValue { get; set; }
    }
}
