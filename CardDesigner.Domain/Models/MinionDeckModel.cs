using CardDesigner.Domain.Interfaces;
using System.Collections.Generic;

namespace CardDesigner.Domain.Models
{
    public class MinionDeckModel : IDeck, ISelectableItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public List<MinionCardModel> MinionCards { get; set; }
        public List<CharacterModel> Characters { get; set; }
    }
}
