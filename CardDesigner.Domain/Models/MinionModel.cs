using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;

namespace CardDesigner.Domain.Models
{
    public class MinionModel : ISelectableItem
    {
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

        //https://www.aidedd.org/dnd/monstres.php?vo=bullywug
        public string Abilities { get; set; } // Parse STR,DEX..
        public string Attributes { get; set; } // Better name? - Description 1
        public string Actions { get; set; } // Descriptions of attacks - Description 2
        public string SavingThrows { get; set; }
        public string Skills { get; set; }
        public string Senses { get; set; }
        public string Languages { get; set; }
        public string ChalangeRating { get; set; }
        public string DamageResistances { get; set; }
        public string DamageImmunities { get; set; }
        public string ConditionImmunities { get; set; }
    }
}
