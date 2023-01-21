﻿using CardDesigner.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class ItemCard
    {
        [Key]
        public int ID { get; set; }

        public ICollection<ItemDeck> ItemDecks { get; set; }
        public List<ItemDeckItemCard> ItemDeckItemCards { get; set; }

        public string Title { get; set; }
        public string Name { get; set; }
    }
}