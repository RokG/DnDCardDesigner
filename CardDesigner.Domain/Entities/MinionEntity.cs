using CardDesigner.Domain.Enums;
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
        //public CharacterAbilitiesEntity Abilities { get; set; }
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
