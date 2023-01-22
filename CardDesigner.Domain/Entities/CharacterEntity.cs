using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class CharacterEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public SpellDeckEntity SpellDeck { get; set; }
        public ItemDeckEntity ItemDeck { get; set; }
    }
}