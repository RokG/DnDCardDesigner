using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;
using System.Collections.Generic;

namespace CardDesigner.Domain.Models
{
    public class ItemDeckModel : ISelectableItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<ItemCardModel> ItemCards { get; set; }

        public DeckType Type { get; set; }
        public ICollection<CharacterModel> Characters { get; set; }

    }
}
