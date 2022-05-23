using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class ItemCard
    {
        [Key]
        public int ID { get; set; }

        public Character Character { get; set; }
    }
}