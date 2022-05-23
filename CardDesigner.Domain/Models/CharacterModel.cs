﻿using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace CardDesigner.Domain.Models
{
    public class CharacterModel
    {
        public string Name { get; set; }

        public int ID { get; set; }

        public List<CardDeckModel> Decks { get; set; } = new List<CardDeckModel>();

        public CharacterModel(string name)
        {
            Name = name;
        }

        public CardDeckModel GetCharacterDeck(DeckType deckType)
        {
            if (Decks.FirstOrDefault(c => c.Type == deckType) == null)
            {
                Decks.Add(new CardDeckModel(deckType.ToString(), deckType));
                return Decks.First(d => d.Type == deckType);
            }
            else
            {
                return Decks.First(c => c.Type == deckType);
            }
        }

        public List<SpellCardModel> GetCharacterDeckCards()
        {
            return GetCharacterDeck(DeckType.Spells).SpellCards;
        }

        public void AddCardToDeck(SpellCardModel card, DeckType deckType)
        {
            //foreach (ICard existingCard in GetCharacterDeckCards(deckType))
            //{
            //    if (existingCard.Conflicts(card))
            //    {
            //        throw new CardDeckConflictException(existingCard, card);
            //    }
            //}

            GetCharacterDeckCards().Add(card);
        }
    }
}