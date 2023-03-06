using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class SpellDeckEntity
    {
        [Key]
        public int ID { get; set; }
        public ICollection<SpellCardEntity> Cards { get; set; }
        public List<SpellDeckSpellCard> SpellDeckSpellCards { get; set; }

        // Properties
        public string Name { get; set; }
        public string Title { get; set; }
    }
}
