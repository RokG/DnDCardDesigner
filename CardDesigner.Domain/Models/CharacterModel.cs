using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Interfaces;
using System.Collections.Generic;

namespace CardDesigner.Domain.Models
{
    public class CharacterModel : ISelectableItem
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public List<SpellDeckDesignLinkerModel> SpellDeckDescriptors { get; set; }
        public List<ItemDeckDesignLinkerModel> ItemDeckDescriptors { get; set; }
        public CharacterDeckDesignModel DeckBackgroundDesign { get; set; }
        public List<CharacterClassModel> Classes { get; set; }
    }
}