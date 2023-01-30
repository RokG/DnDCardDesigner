using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class ItemDeckDesignEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<CharacterEntity> Characters { get; set; }
        public string FrontLineColor { get; set; }
        public string FrontBackgroundColor { get; set; }
        public string FrontHeaderTextColor { get; set; } = "#475356";
        public string FrontHeaderIconColor { get; set; } = "#475356";
        public string FrontDescriptionTextColor { get; set; } = "#475356";
        public string FrontFooterTextColor { get; set; } = "#475356";
        public string FrontFooterIconColor { get; set; } = "#475356";
        public string FrontForegroundColor { get; set; }
        public string FrontHeaderColor { get; set; }
        public string FrontFooterColor { get; set; }
        public string FrontHiglightColor { get; set; }
        public string BackLineColor { get; set; }
        public int BackLineThickness { get; set; }
        public string BackBackgroundColor { get; set; }
        public string BackgroundIconFilePath { get; set; }
        public string BackgroundIconStretch { get; set; }
    }
}
