﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class SpellDeckDesignEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<CharacterEntity> Characters { get; set; }
        public string HeaderColor { get; set; } = "#d5b895";
        public string HeaderTextColor { get; set; } = "#222222";
        public string HeaderIconColor { get; set; } = "#222222";
        public string BackgroundColor { get; set; } = "#ffffff";
        public string DescriptionTextColor { get; set; } = "#222222";
        public string LineColor { get; set; } = "#222222";
        public string FooterColor { get; set; } = "#d5b895";
        public string FooterTextColor { get; set; } = "#222222";
        public string FooterIconColor { get; set; } = "#222222";
    }
}
