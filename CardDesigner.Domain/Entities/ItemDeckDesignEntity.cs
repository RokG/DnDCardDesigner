using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class ItemDeckDesignEntity
    {
        [Key]
        public int ID { get; set; }

        // Properties
        public CharacterEntity Character { get; set; }
        public int ItemDeckID { get; set; }
        public int DesignID { get; set; }
    }
}
