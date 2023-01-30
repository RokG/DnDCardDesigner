using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class CharacterEntity
    {
        [Key]
        public int ID { get; set; }

        // Properties
        public string Name { get; set; }
        public List<SpellDeckDesignEntity> SpellDeckDescriptors { get; set; }
        public List<ItemDeckDesignEntity> ItemDeckDescriptors { get; set; }
        public CardDesignEntity DeckBackgroundDesign { get; set; }
    }
}