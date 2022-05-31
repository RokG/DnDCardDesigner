using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Models
{
    public class SpellDeckModel : ISelectableItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<SpellCardModel> SpellCards { get; set; }

        public DeckType Type { get; set; }
        public ICollection<CharacterModel> Characters { get; set; }

    }
}
