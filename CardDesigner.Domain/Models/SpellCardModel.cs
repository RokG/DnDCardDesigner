﻿using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;

namespace CardDesigner.Domain.Models
{
    public class SpellCardModel : ICard, ISpell, ISelectableItem
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; } = 0;
        public MagicSchool School { get; set; } = MagicSchool.None;
        public bool HasVerbalComponent { get; set; } = false;
        public bool HasMaterialComponent { get; set; }= false;
        public bool HasSomaticComponent { get; set; }= false;
        public bool IsRitual { get; set; } = false;
        public bool IsConcentration { get; set; } = false ;
        public CastingTimeType CastingTimeType { get; set; } = CastingTimeType.Action;
        public int CastingTimeValue { get; set; } = 1;
        public RangeType RangeType { get; set; } = RangeType.Self;
        public int RangeValue { get; set; } = 0;
        public DurationType DurationType { get; set; } = DurationType.Instantaneous;
        public int DurationValue { get; set; } = 1;
        public DiceType DiceType { get; set; } = DiceType.d20;
        public int DiceValue { get; set;} = 0;
        public AreaOfEffect AreaOfEffect { get; set; } = AreaOfEffect.Sphere;
        public int AreaOfEffectValue { get; set; }  
        public TargetType TargetType { get; set; } = TargetType.Self;
        public string Target { get; set; } = string.Empty;
        public DamageType DamageType { get; set; } = DamageType.Fire;
    }
}