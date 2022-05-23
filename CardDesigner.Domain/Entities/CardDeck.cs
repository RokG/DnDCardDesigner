using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Entities
{
    public class CardDeck
    {
        [Key]
        public int ID { get; set; }

        public DeckType Type { get; set; }

        public string Name { get; set; }

        public Character Character { get; set; }

        public List<SpellCard> SpellCards { get; set; } = new List<SpellCard>();
        public List<FeatCard> FeatCards { get; set; } = new List<FeatCard>();
        public List<ItemCard> ItemCards { get; set; } = new List<ItemCard>();
    }
}