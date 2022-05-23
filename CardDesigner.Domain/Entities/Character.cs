using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Entities
{
    public class Character
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public List<CardDeck> Decks { get; set; } = new List<CardDeck>();
    }
}