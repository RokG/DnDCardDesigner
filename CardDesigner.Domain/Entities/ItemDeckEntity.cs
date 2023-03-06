using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class ItemDeckEntity
    {
        [Key]
        public int ID { get; set; }
        public ICollection<ItemCardEntity> Cards { get; set; }
        public List<ItemDeckItemCard> ItemDeckItemCards { get; set; }

        // Properties
        public string Name { get; set; }
        public string Title { get; set; }
    }
}
