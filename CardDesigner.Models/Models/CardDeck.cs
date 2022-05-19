using CardDesigner.Domain.Enums;
using System.Collections.Generic;

namespace CardDesigner.Domain.Models
{
    public class CardDeck
    {
        private readonly List<ICard> _cards;

        public DeckType Type { get; set; }

        public string Name { get; set; }

        public int ID { get; set; }

        public List<ICard> Cards { get; set; } = new List<ICard>();

        public CardDeck(string name, DeckType type)
        {
            Name = name;
            _cards = new();
            Type = type;
        }
    }
}