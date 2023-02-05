namespace CardDesigner.Domain.Models
{
    public class SpellDeckDesignLinkerModel
    {
        public int ID { get; set; }
        public CharacterModel Character { get; set; }
        public int SpellDeckID { get; set; }
        public int DesignID { get; set; }
    }
}
