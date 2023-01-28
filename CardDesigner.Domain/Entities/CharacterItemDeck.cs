namespace CardDesigner.Domain.Entities
{
    public class CharacterItemDeck
    {
        public int CharacterID { get; set; }
        public CharacterEntity Character { get; set; }
        public int ItemDeckID { get; set; }
        public ItemDeckEntity ItemDeck { get; set; }
    }
}
