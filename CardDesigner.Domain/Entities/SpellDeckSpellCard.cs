using CardDesigner.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Entities
{
    public class SpellDeckSpellCard
    {
        public int SpellDeckID { get; set; }
        public SpellDeck SpellDeck { get; set; }

        public int SpellCardID { get; set; }
        public SpellCard SpellCard { get; set; }
    }
}
