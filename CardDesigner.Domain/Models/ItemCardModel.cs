using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;

namespace CardDesigner.Domain.Models
{
    public class ItemCardModel : ICard, ISelectableItem
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; }
        public double TitleFontSize { get; set; } = 14;
        public string Description { get; set; } = string.Empty;
        public double DescriptionFontSize { get; set; } = 16;
        public ItemType Type { get; set; }
        public IItem Item { get; set; }
        public string ItemID { get; set; }
        public string IconFilePath { get; set; } = string.Empty;
        public string IconStretch { get; set; } = "Uniform";
        public int Level { get; set; } = 0;
        public bool IsUnidentified { get; set; }
        public bool RequiresAttunement { get; set; }
        public bool IsMagical { get; set; }
    }
}