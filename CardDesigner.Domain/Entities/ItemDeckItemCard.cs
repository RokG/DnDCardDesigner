using CardDesigner.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Entities
{
    public class ItemDeckItemCard
    {
        public int ItemDeckID { get; set; }
        public ItemDeck ItemDeck { get; set; }

        public int ItemCardID { get; set; }
        public ItemCard ItemCard { get; set; }
    }
}
