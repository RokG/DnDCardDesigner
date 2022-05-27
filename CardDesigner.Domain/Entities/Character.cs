using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class Character
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public SpellDeck SpellDeck { get; set; }
    }
}