using CardDesigner.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class CharacterEntity
    {
        [Key]
        public int ID { get; set; }

        // Properties
        public string Name { get; set; }
        public string Title { get; set; }
        public List<SpellDeckDesignLinkerEntity> SpellDeckDescriptors { get; set; }
        public List<ItemDeckDesignLinkerEntity> ItemDeckDescriptors { get; set; }
        public CharacterDeckDesignEntity DeckBackgroundDesign { get; set; }
        public List<CharacterClassEntity> Classes { get; set; }
        public CharacterAttributesEntity Attributes { get; set; }

        public Race Race { get; set; }
        public Alignment Alignment { get; set; }

        public string Height { get; set; }
        public string Weight { get; set; }
        public string Eyes { get; set; }
        public string Skin { get; set; }
        public string Hair { get; set; }

        public string AvatarImagePath { get; set; }
        public string AvatarImageStretch { get; set; }

        public int Proficiency { get; set; }
        public int PassivePerception { get; set; }
        public int PassiveInsight { get; set; }

        public bool IsLightArmourProficiency { get; set; }
        public bool IsMediumArmourProficient { get; set; }
        public bool IsHeavyArmourProficient { get; set; }
        public bool IsShieldProficient { get; set; }
        public bool IsSimpleWeaponProficient { get; set; }
        public bool IsMartialWeaponProficient { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Inteligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        public int Health { get; set; }
        public int Initiative { get; set; }
        public int Speed { get; set; }
        public int ArmourClass { get; set; }
        public int SpellAttackBonus { get; set; }
        public int SpellSaveDC { get; set; }
    }
}