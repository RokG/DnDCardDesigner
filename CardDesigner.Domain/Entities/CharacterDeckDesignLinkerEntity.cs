using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class CharacterDeckDesignLinkerEntity
    {
        [Key]
        public int ID { get; set; }

        // Properties
        public CharacterEntity Character { get; set; }
        public int CharacterDeckID { get; set; }
        public int DesignID { get; set; }
    }
}
