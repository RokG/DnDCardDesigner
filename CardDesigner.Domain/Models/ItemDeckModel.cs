using CardDesigner.Domain.Interfaces;
using System.Collections.Generic;

namespace CardDesigner.Domain.Models
{
    public class ItemDeckModel : ISelectableItem, IDeck
    {
        // Entity links
        public int ID { get; set; }

        // Properties
        public string Name { get; set; }
        public string Title { get; set; }
        public List<ItemCardModel> Cards { get; set; }
    }
}
