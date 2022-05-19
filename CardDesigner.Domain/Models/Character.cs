using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace CardDesigner.Domain.Models
{
    public class Character
    {
        public string Name { get; set; }

        public int ID { get; set; }

        public List<CardDeck> Decks { get; set; } = new List<CardDeck>();

        public Character(string name)
        {
            Name = name;
        }

        public CardDeck GetCharacterDeck(DeckType deckType)
        {
            if (Decks.FirstOrDefault(c => c.Type == deckType) == null)
            {
                Decks.Add(new CardDeck(deckType.ToString(), deckType));
                return Decks.First(d => d.Type == deckType);
            }
            else
            {
                return Decks.First(c => c.Type == deckType);
            }
        }

        public List<ICard> GetCharacterDeckCards(DeckType deckType)
        {
            return GetCharacterDeck(deckType).Cards;
        }

        public void AddCardToDeck(ICard card, DeckType deckType)
        {
            //foreach (ICard existingCard in GetCharacterDeckCards(deckType))
            //{
            //    if (existingCard.Conflicts(card))
            //    {
            //        throw new CardDeckConflictException(existingCard, card);
            //    }
            //}

            GetCharacterDeckCards(deckType).Add(card);
        }
    }
}