using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media.TextFormatting;

namespace CardDesigner.Domain.Entities
{
    public class CharacterEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public CardDesignEntity CardDesign { get; set; }
        public List<SpellDeckEntity> SpellDecks { get; set; }
        public List<CharacterSpellDeck> CharacterSpellDeck { get; set; }
        public List<ItemDeckEntity> ItemDecks { get; set; }
        public List<CharacterItemDeck> CharacterItemDeck { get; set; }
    }
}