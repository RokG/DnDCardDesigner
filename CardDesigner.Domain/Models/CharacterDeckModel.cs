using CardDesigner.Domain.Interfaces;
using System.Collections.Generic;

namespace CardDesigner.Domain.Models
{
    public class CharacterDeckModel : IDeck, ISelectableItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<CharacterCardModel> CharacterCards { get; set; }
        public List<CharacterModel> Characters { get; set; }
    }
}
