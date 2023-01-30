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
        public List<SpellDeckDesignLinkerEntity> SpellDeckDescriptors { get; set; }
        public List<ItemDeckDesignLinkerEntity> ItemDeckDescriptors { get; set; }
        public CharacterDeckDesignEntity DeckBackgroundDesign { get; set; }
    }
}