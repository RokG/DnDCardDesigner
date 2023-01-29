using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class SpellDeckDesignEntity
    {
        [Key]
        public int ID { get; set; }

        // Properties
        public CharacterEntity Character { get; set; }
        public int SpellDeckID { get; set; }
        public int DesignID { get; set; }
    }
}
