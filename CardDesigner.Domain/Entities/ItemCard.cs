using CardDesigner.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class ItemCard
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public Character Owner { get; set; }
        public string Name { get; set; }
        public CardType Type { get; set; }



    }
}