namespace CardDesigner.Domain.Entities
{
    public class CharacterSpellDeck
    {
        public int CharacterID { get; set; }
        public CharacterEntity Character { get; set; }
        public int SpellDeckID { get; set; }
        public SpellDeckEntity SpellDeck { get; set; }
    }
}
