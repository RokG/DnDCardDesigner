using System.Collections.Generic;

namespace CardDesigner.Domain.Models
{
    public class CharacterModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public SpellDeckModel SpellDeck { get; set; }
    }
}