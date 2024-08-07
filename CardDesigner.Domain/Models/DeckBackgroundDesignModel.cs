﻿using CardDesigner.Domain.Interfaces;

namespace CardDesigner.Domain.Models
{
    public class DeckBackgroundDesignModel : ISelectableItem, ICardDesign
    {
        // Entity links
        public int ID { get; set; }

        // Properties
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
        public int BackLineThickness { get; set; } = 3;
        public string IconFilePath { get; set; } = string.Empty;
        public string IconStretch { get; set; } = "Uniform";
    }
}
