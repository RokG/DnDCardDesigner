using CardDesigner.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class SpellDeckEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<SpellCardEntity> SpellCards { get; set; }
        public List<SpellDeckSpellCard> SpellDeckSpellCards { get; set; }
        public DeckType Type { get; set; }
        public ICollection<CharacterEntity> Characters { get; set; }
    }
}
