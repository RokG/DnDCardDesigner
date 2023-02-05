namespace CardDesigner.Domain.Models
{
    public class ItemDeckDesignLinkerModel
    {
        public int ID { get; set; }
        public CharacterModel Character { get; set; }
        public int ItemDeckID { get; set; }
        public int DesignID { get; set; }
    }
}
