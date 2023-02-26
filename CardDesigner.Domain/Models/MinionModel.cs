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
        //public CharacterAbilitiesModel Abilities { get; set; }
        public Race Race { get; set; }
        public Alignment Alignment { get; set; }
        public int Hitpoints { get; set; }
        public int ArmourClass { get; set; }
        public int FlyingSpeed { get; set; }
        public int Speed { get; set; }
        public int ClimbingSpeed { get; set; }
        public int SwimingSpeed { get; set; }
        public int Initiative { get; set; }
        public int PassivePerception { get; set; }
        public int PassiveInsight { get; set; }
    }
}
