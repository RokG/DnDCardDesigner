using System.Collections.Generic;

namespace CardDesigner.Domain.Models
{
    public interface ISpell
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Level { get; set; }

        public MagicSchool School { get; set; }

        public bool HasVerbalComponent { get; set; }

        public bool HasSomaticComponent { get; set; }

        public bool HasMaterialComponent { get; set; }

        public bool IsRitual { get; set; }

        public bool IsConcentration { get; set; }

        public int CastingTimeValue { get; set; }

        public CastingTimeType CastingTimeType { get; set; }

        public int RangeValue { get; set; }

        public RangeType RangeType { get; set; }

        public string Target { get; set; }
    }
}