using CardDesigner.Domain.Enums;

namespace CardDesigner.Domain.Models
{
    public interface ICard
    {
        public CardType Type  { get; set; }

        public string Name { get; set; }

        public int ID { get; set; }

        public string Title { get; set; }
    }
}