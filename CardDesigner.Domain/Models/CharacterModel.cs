using System.Collections.Generic;

namespace CardDesigner.Domain.Models
{
    public class CharacterModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public List<SpellDeckModel> SpellDecks { get; set; } = new List<SpellDeckModel>();

        public List<SpellDeckModel> ItemDecks { get; set; } = new List<SpellDeckModel>();

        public CharacterModel(string name)
        {
            Name = name;
        }
    }
}