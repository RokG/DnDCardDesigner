using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class MinionCardEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<MinionDeckEntity> MinionDecks { get; set; }
        public List<MinionDeckMinionCard> MinionDeckMinionCards { get; set; }
        public string Title { get; set; }

        public MinionEntity Minion { get; set; }
    }
}
