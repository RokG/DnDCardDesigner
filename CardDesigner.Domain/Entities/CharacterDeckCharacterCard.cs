namespace CardDesigner.Domain.Entities
{
    public class CharacterDeckCharacterCard
    {
        public int CharacterDeckID { get; set; }
        public CharacterDeckEntity CharacterDeck { get; set; }
        public int CharacterCardID { get; set; }
        public CharacterCardEntity CharacterCard { get; set; }
    }
}
