namespace CardDesigner.Domain.Models
{
    public class SpellDeckDesignLinkerModel
    {
        // Entity links
        public int ID { get; set; }

        // Properties
        public CharacterModel Character { get; set; }
        public int SpellDeckID { get; set; }
        public int DesignID { get; set; }
    }
}
