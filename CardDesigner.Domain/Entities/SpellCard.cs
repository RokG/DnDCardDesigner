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
        public int CastingTimeValue { get; set; }
        public CastingTimeType CastingTimeType { get; set; }
        public int RangeValue { get; set; }
        public RangeType RangeType { get; set; }
        public string Target { get; set; } = string.Empty;
    }
}