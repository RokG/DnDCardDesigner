using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class CharacterDeckEntity
    {
        [Key]
        public int ID { get; set; }
        public ICollection<CharacterCardEntity> CharacterCards { get; set; }
        public List<CharacterDeckCharacterCard> CharacterDeckCharacterCards { get; set; }

        // Properties
        public string Name { get; set; }
    }
}
