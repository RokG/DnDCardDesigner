using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class ItemDeckEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<ItemCardEntity> ItemCards { get; set; }
        public List<ItemDeckItemCard> ItemDeckItemCards { get; set; }
        public ICollection<CharacterEntity> Characters { get; set; }
    }
}
