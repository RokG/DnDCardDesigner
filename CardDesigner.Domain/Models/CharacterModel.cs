using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CardDesigner.Domain.Models
{
    public class CharacterModel : ISelectableItem
    {
        public int ID { get; set; }
        public CharacterModel Character { get; set; }

        public string Name { get; set; }
        public string Title { get; set; }
        public List<SpellDeckDesignLinkerModel> SpellDeckDescriptors { get; set; } = new();
        public List<ItemDeckDesignLinkerModel> ItemDeckDescriptors { get; set; } = new();
        public CharacterDeckDesignModel CharacterDeckDesign { get; set; } = new();
        public DeckBackgroundDesignModel DeckBackgroundDesign { get; set; } = new();
        public List<CharacterClassModel> Classes { get; set; } = new();
        public CharacterAbilitiesModel Abilities { get; set; } = new();
        public CasterStatsModel CasterStats { get; set; } = new();

        public Race Race { get; set; }
        public Alignment Alignment { get; set; }

        public string AvatarImagePath { get; set; } = string.Empty;
        public string AvatarImageStretch { get; set; } = "Uniform";

        public string Height { get; set; }
        public string Weight { get; set; }
        public string Age { get; set; }
        public int Hitpoints { get; set; }

        public int Proficiency { get; set; }
        public int PassivePerception { get; set; }
        public int PassiveInsight { get; set; }
        public int ArmourClass { get; set; }
        public int Initiative { get; set; }
        public int Speed { get; set; }

        public bool IsLightArmourProficiency { get; set; }
        public bool IsMediumArmourProficient { get; set; }
        public bool IsHeavyArmourProficient { get; set; }
        public bool IsShieldProficient { get; set; }
        public bool IsSimpleWeaponProficient { get; set; }
        public bool IsMartialWeaponProficient { get; set; }
        public string OtherProficiencies { get; set; }

    }
}