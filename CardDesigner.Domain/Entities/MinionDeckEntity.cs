using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class MinionDeckEntity
    {
        [Key]
        public int ID { get; set; }
        public ICollection<MinionCardEntity> Cards { get; set; }
        public List<MinionDeckMinionCard> MinionDeckMinionCards { get; set; }

        // Properties
        public string Name { get; set; }
        public string Title { get; set; }
    }
}
