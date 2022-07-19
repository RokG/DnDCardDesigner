using CardDesigner.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class SpellCard
    {
        [Key]
        public int ID { get; set; }

        public ICollection<SpellDeck> SpellDecks { get; set; }
        public List<SpellDeckSpellCard> SpellDeckSpellCards { get; set; }
        
        public string Title { get; set; } = string.Empty;
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public MagicSchool School { get; set; }
        public bool HasVerbalComponent { get; set; }
        public bool HasMaterialComponent { get; set; }
        public bool HasSomaticComponent { get; set; }
        public bool IsRitual { get; set; }
        public bool IsConcentration { get; set; }
        public CastingTimeType CastingTimeType { get; set; } = CastingTimeType.Action;
        public int CastingTimeValue { get; set; } = 1;
        public RangeType RangeType { get; set; } = RangeType.Self;
        public int RangeValue { get; set; } = 0;
        public DurationType DurationType { get; set; } = DurationType.Instantaneous;
        public int DurationValue { get; set; } = 1;
        public DiceType DiceType { get; set; } = DiceType.d20;
        public int DiceValue { get; set; } = 0;
        public AreaOfEffect AreaOfEffect { get; set; } = AreaOfEffect.Sphere;
        public int AreaOfEffectValue { get; set; }
        public TargetType TargetType { get; set; } = TargetType.Self;
        public string Target { get; set; } = string.Empty;
        public DamageType DamageType { get; set; } = DamageType.Fire;
    }
}