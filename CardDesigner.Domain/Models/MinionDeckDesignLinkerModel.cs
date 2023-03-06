namespace CardDesigner.Domain.Models
{
    public class MinionDeckDesignLinkerModel
    {
        // Entity links
        public int ID { get; set; }

        // Properties
        public CharacterModel Character { get; set; }
        public int MinionDeckID { get; set; }
        public int DesignID { get; set; }
    }
}
