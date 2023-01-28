using CardDesigner.Domain.Interfaces;
using System.Collections.Generic;

namespace CardDesigner.Domain.Models
{
    public class SpellDeckModel : ISelectableItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<SpellCardModel> SpellCards { get; set; }
        public ICollection<CharacterModel> Characters { get; set; }
    }
}
