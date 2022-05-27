using CardDesigner.Domain.Enums;

namespace CardDesigner.Domain.Models
{
    public class ItemCardModel : ICard, IItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public CardType Type { get; set; }
    }
}