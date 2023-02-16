namespace CardDesigner.Domain.Models
{
    public class CharacterDeckDesignLinkerModel
    {
        public int ID { get; set; }
        public CharacterModel Character { get; set; }
        public int CharacterDeckID { get; set; }
        public int DesignID { get; set; }
    }
}
