using CardDesigner.Domain.Interfaces;
using System.Collections.Generic;

namespace CardDesigner.Domain.Models
{
    public class CharacterDeckModel : IDeck, ISelectableItem
    {
        // Entity links
        public int ID { get; set; }

        // Properties
        public string Name { get; set; }
        public string Title { get; set; }
        public List<CharacterCardModel> Cards { get; set; }
    }
}
