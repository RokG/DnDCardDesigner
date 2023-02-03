using CardDesigner.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class CharacterClassEntity
    {
        [Key]
        public int ID { get; set; }

        // Properties
        public int Level { get; set; }
        public CharacterClassType Class { get; set; }
        public CharacterEntity Character { get; set; }
    }
}
