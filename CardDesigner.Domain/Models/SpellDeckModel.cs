using CardDesigner.Domain.Interfaces;
using System.Collections.Generic;

namespace CardDesigner.Domain.Models
{
    public class SpellDeckModel : ISelectableItem, IDeck
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public List<SpellCardModel> SpellCards { get; set; }
        public List<CharacterModel> Characters { get; set; }
    }
}
