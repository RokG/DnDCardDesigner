namespace CardDesigner.Domain.Models
{
    public class ItemDeckDesignLinkerModel
    {
        // Entity links
        public int ID { get; set; }

        // Properties
        public CharacterModel Character { get; set; }
        public int ItemDeckID { get; set; }
        public int DesignID { get; set; }
    }
}
