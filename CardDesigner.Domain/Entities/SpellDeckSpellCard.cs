namespace CardDesigner.Domain.Entities
{
    public class SpellDeckSpellCard
    {
        public int SpellDeckID { get; set; }
        public SpellDeckEntity SpellDeck { get; set; }
        public int SpellCardID { get; set; }
        public SpellCardEntity SpellCard { get; set; }
    }
}
