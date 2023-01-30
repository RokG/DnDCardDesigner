using CardDesigner.Domain.Interfaces;

namespace CardDesigner.Domain.Models
{
    public class SpellDeckDesignModel : ISelectableItem, ICardDesign
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FrontLineColor { get; set; } = "#475356";
        public string FrontBackgroundColor { get; set; } = "#475356";
        public string FrontHeaderTextColor { get; set; } = "#475356";
        public string FrontHeaderIconColor { get; set; } = "#475356";
        public string FrontDescriptionTextColor { get; set; } = "#475356";
        public string FrontFooterTextColor { get; set; } = "#475356";
        public string FrontFooterIconColor { get; set; } = "#475356";
        public string FrontForegroundColor { get; set; } = "#475356";
        public string FrontHeaderColor { get; set; } = "#475356";
        public string FrontFooterColor { get; set; } = "#475356";
        public string FrontHiglightColor { get; set; } = "#475356";
        public string BackLineColor { get; set; } = "#475356";
        public int BackLineThickness { get; set; } = 3;
        public string BackBackgroundColor { get; set; } = "#475356";
        public string BackgroundIconFilePath { get; set; } = string.Empty;
        public string BackgroundIconStretch { get; set; } = "Uniform";
    }
}
