namespace CardDesigner.Domain.Models
{
    public class CharacterDeckDesignLinkerModel
    {
        // Entity links
        public int ID { get; set; }

        // Properties
        public CharacterModel Character { get; set; }
        public int CharacterDeckID { get; set; }
        public int DesignID { get; set; }
    }
}
