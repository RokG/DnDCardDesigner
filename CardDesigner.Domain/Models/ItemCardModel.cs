using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;

namespace CardDesigner.Domain.Models
{
    public class ItemCardModel : ICard, IItem, ISelectableItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
    }
}