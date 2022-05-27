using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class Character
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public List<SpellDeck> SpellDecks { get; set; } = new List<SpellDeck>();
        public List<SpellDeck> ItemDecks { get; set; } = new List<SpellDeck>();
    }
}