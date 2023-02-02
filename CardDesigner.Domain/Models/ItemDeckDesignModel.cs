using CardDesigner.Domain.Interfaces;

namespace CardDesigner.Domain.Models
{
    public class ItemDeckDesignModel : ISelectableItem, ICardDesign
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string HeaderColor { get; set; } = "#d5b895";
        public string HeaderTextColor { get; set; } = "#222222";
        public string HeaderIconColor { get; set; } = "#222222";
        public string BackgroundColor { get; set; } = "#ffffff";
        public string DescriptionTextColor { get; set; } = "#222222";
        public string LineColor { get; set; } = "#222222";
        public string FooterColor { get; set; } = "#d5b895";
        public string FooterTextColor { get; set; } = "#222222";
        public string FooterIconColor { get; set; } = "#222222";
        public string IconFilePath { get; set; } = string.Empty;
        public string IconStretch { get; set; } = "Uniform";
    }
}
