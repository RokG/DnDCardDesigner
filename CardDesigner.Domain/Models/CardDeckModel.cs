using CardDesigner.Domain.Enums;
using System.Collections.Generic;

namespace CardDesigner.Domain.Models
{
    public class CardDeckModel
    {
        private readonly List<ICard> _cards;

        public DeckType Type { get; set; }

        public string Name { get; set; }

        public int ID { get; set; }

        public List<SpellCardModel> SpellCards { get; set; } = new List<SpellCardModel>();
        public List<ItemCardModel> ItemCards { get; set; } = new List<ItemCardModel>();
        public List<FeatCardModel> FeatCards { get; set; } = new List<FeatCardModel>();

        public CardDeckModel(string name, DeckType type)
        {
            Name = name;
            _cards = new();
            Type = type;
        }
    }
}