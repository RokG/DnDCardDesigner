using CardDesigner.Domain.Interfaces;
using System.Collections.Generic;

namespace CardDesigner.Domain.Models
{
    public class CharacterModel : ISelectableItem
    {
        public int ID { get; set; }
        public CharacterModel Character { get; set; }

        public string Name { get; set; }
        public string Title { get; set; }
        public List<SpellDeckDesignLinkerModel> SpellDeckDescriptors { get; set; }
        public List<ItemDeckDesignLinkerModel> ItemDeckDescriptors { get; set; }
        public CharacterDeckDesignModel DeckBackgroundDesign { get; set; }
        public List<CharacterClassModel> Classes { get; set; }

        public string AvatarImagePath { get; set; } = string.Empty;
        public string AvatarImageStretch { get; set; } = "Uniform";

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

        // TODO
        //public List<ToolProficiency> ToolProficiencies { get; set; }
        public List<CharacterSkillModel> Skills { get; set; }
    }
}