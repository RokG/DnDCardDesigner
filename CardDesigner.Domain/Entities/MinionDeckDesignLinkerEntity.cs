using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class MinionDeckDesignLinkerEntity
    {
        [Key]
        public int ID { get; set; }

        // Properties
        public CharacterEntity Character { get; set; }
        public int MinionDeckID { get; set; }
        public int DesignID { get; set; }
    }
}
