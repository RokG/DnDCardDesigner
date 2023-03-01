﻿using CardDesigner.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class MinionEntity
    {
        [Key]
        public int ID { get; set; }

        // Properties
        public string Name { get; set; }
        public string Title { get; set; }
        public Alignment Alignment { get; set; }
        public string Appearance { get; set; } // Small humanoid
        public int Hitpoints { get; set; }
        public int ArmourClass { get; set; }
        public int Speed { get; set; }
        public int FlyingSpeed { get; set; }
        public int ClimbingSpeed { get; set; }
        public int SwimingSpeed { get; set; }
        public int Initiative { get; set; }
        public int PassivePerception { get; set; }

        public string AvatarImagePath { get; set; } = string.Empty;
        public string AvatarImageStretch { get; set; } = "Uniform";

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Inteligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        public string Attributes { get; set; }
        public string Actions { get; set; }
        public string LegendaryActions { get; set; }
        public string SavingThrows { get; set; }
        public string SkillBonuses { get; set; }
        public string DamageImmunities { get; set; }
        public string DamageResistances { get; set; }
        public string ConditionImmunities { get; set; }
        public string Senses { get; set; }
        public string Languages { get; set; }
        public string ChalangeRating { get; set; }
    }
}