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
    public class SpellDeck
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<SpellCard> SpellCards { get; set; }
        public List<SpellDeckSpellCard> SpellDeckSpellCards { get; set; }

        public DeckType Type { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}
