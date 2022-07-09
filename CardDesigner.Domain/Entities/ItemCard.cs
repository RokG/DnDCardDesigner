using CardDesigner.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class ItemCard
    {
        [Key]
        public int ID { get; set; }

        public ICollection<SpellDeck> Decks { get; set; }
        public List<SpellDeckSpellCard> DeckCards { get; set; }

        public string Title { get; set; }
        public string Name { get; set; }
    }
}