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

        //public DeckType Type { get; set; }

        public string Name { get; set; }

        //public List<SpellCard> Cards { get; set; } = new List<SpellCard>();
    }
}