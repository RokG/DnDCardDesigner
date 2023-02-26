using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;

namespace CardDesigner.Domain.Models
{
    public class MinionCardModel : ICard, ISelectableItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public MinionModel Minion { get; set; }
        public MinionCardType Type { get; set; }
    }
}
