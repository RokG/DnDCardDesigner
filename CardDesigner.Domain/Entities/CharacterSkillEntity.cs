using CardDesigner.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class CharacterSkillEntity
    {
        [Key]
        public int ID { get; set; }
        public CharacterEntity Character { get; set; }

        public Skill Skill { get; set; }
        public bool IsProficient { get; set; }
        public bool IsExpert { get; set; }
    }
}
