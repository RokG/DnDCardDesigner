namespace CardDesigner.Domain.Entities
{
    public class MinionDeckMinionCard
    {
        public int MinionDeckID { get; set; }
        public MinionDeckEntity MinionDeck { get; set; }
        public int MinionCardID { get; set; }
        public MinionCardEntity MinionCard { get; set; }
    }
}
