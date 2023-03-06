using CardDesigner.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class MinionCardEntity
    {
        [Key]
        public int ID { get; set; }
        public ICollection<MinionDeckEntity> MinionDecks { get; set; }
        public List<MinionDeckMinionCard> MinionDeckMinionCards { get; set; }

        // Properties
        public string Name { get; set; }
        public string Title { get; set; }
        public MinionEntity Minion { get; set; }
        public MinionCardType Type { get; set; }
        public double TitleFontSize { get; set; } = 14;
        public double DescriptionFontSize { get; set; } = 16;
    }
}
