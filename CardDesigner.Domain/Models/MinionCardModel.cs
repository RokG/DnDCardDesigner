using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;

namespace CardDesigner.Domain.Models
{
    public class MinionCardModel : ICard, ISelectableItem
    {
        // Entity links
        public int ID { get; set; }

        // Properties
        public string Name { get; set; }
        public string Title { get; set; }
        public MinionModel Minion { get; set; }
        public MinionCardType Type { get; set; }
        public double TitleFontSize { get; set; } = 14;
        public double DescriptionFontSize { get; set; } = 16;
    }
}
