using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;

namespace CardDesigner.Domain.Models
{
    public class CharacterCardModel : ICard, ISelectableItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Title { get; set; }
        public double TitleFontSize { get; set; } = 14;
        public string Description { get; set; } = string.Empty;
        public double DescriptionFontSize { get; set; } = 16;
        public CharacterCardType Type { get; set; }
    }
}
