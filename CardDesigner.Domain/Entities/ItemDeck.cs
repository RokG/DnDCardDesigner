using CardDesigner.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class ItemDeck
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<ItemCard> ItemCards { get; set; }
        public List<ItemDeckItemCard> ItemDeckItemCards { get; set; }

        public DeckType Type { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}
