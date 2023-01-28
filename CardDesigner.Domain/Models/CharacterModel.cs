using CardDesigner.Domain.Interfaces;
using System.Collections.Generic;

namespace CardDesigner.Domain.Models
{
    public class CharacterModel : ISelectableItem
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public List<SpellDeckModel> SpellDecks { get; set; }
        public List<ItemDeckModel> ItemDecks { get; set; }
    }
}