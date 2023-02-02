using CardDesigner.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class CharacterCardEntity
    {
        [Key]
        public int ID { get; set; }
        public ICollection<CharacterDeckEntity> CharacterDecks { get; set; }
        public List<CharacterDeckCharacterCard> CharacterDeckCharacterCards { get; set; }

        // Properties
        public string Name { get; set; }
        public CharacterCardType Type { get; set; }
    }
}
