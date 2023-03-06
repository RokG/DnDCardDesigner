namespace CardDesigner.Domain.Models
{
    public class MinionDeckDesignLinkerModel
    {
        public int ID { get; set; }
        public CharacterModel Character { get; set; }
        public int MinionDeckID { get; set; }
        public int DesignID { get; set; }
    }
}
