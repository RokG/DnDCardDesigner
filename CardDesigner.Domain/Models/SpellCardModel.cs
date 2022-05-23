using CardDesigner.Domain.Enums;

namespace CardDesigner.Domain.Models
{
    public class SpellCardModel : ICard, ISpell
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
        public MagicSchool School { get; set; }
        public bool HasVerbalComponent { get; set; }
        public bool HasSemanticComponent { get; set; }
        public bool HasMaterialComponent { get; set; }
        public int CastingTimeValue { get; set; }
        public CastingTimeType CastingTimeType { get; set; }
        public int RangeValue { get; set; }
        public RangeType RangeType { get; set; }
        public string Target { get; set; } = string.Empty;
        public bool HasSomaticComponent { get; set; }
        public bool IsRitual { get; set; }
        public bool IsConcentration { get; set; }
        public int ID { get; set; }
    }
}