using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class CharacterClassEntity
    {
        [Key]
        public int ID { get; set; }
        public CharacterEntity Character { get; set; }

        // Properties
        public int Level { get; set; }
        public string ClassID { get; set; }
        public string ClassSpecialization { get; set; }
    }
}
