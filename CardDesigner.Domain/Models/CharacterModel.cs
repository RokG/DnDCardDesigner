using CardDesigner.Domain.Interfaces;
using System.Collections.Generic;

namespace CardDesigner.Domain.Models
{
    public class CharacterModel : ISelectableItem
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public List<SpellDeckDesignModel> SpellDeckDescriptors { get; set; }
        public List<ItemDeckDesignModel> ItemDeckDescriptors { get; set; }
    }
}