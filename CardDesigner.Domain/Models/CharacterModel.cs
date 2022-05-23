using System.Collections.Generic;

namespace CardDesigner.Domain.Models
{
    public class CharacterModel
    {
        public string Name { get; set; }

        public int ID { get; set; }

        public List<SpellCardModel> SpellCards { get; set; } = new List<SpellCardModel>();
        public List<ItemCardModel> ItemCards { get; set; } = new List<ItemCardModel>();

        public CharacterModel(string name)
        {
            Name = name;
        }

        public List<SpellCardModel> GetCharacterSpellDeck()
        {
            return SpellCards;
        }

        public List<ItemCardModel> GetCharacterItemDeck()
        {
            return ItemCards;
        }

        public void AddSpellCardToDeck(SpellCardModel card)
        {
            GetCharacterSpellDeck().Add(card);
        }

        public void AddItemCardToDeck(ItemCardModel card)
        {
            GetCharacterItemDeck().Add(card);
        }
    }
}