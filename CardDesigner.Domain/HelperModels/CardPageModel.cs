using CardDesigner.Domain.Models;
using System.Collections.Generic;

namespace CardDesigner.Domain.HelperModels
{
    public class CardPageModel
    {
        public string Name { get; set; }
        public List<ICard> Cards { get; set; } = new();
    }
}
