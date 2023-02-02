using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;

namespace CardDesigner.Domain.Models
{
    public class CharacterCardModel : ICard, ISelectableItem
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Title { get; set; }
        public CharacterCardType Type { get; set; }
    }
}
