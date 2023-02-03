using CardDesigner.Domain.Enums;

namespace CardDesigner.Domain.Models
{
    public class CharacterSkillModel
    {
        public int ID { get; set; }
        public Skill Skill { get; set; }
        public bool IsProficient { get; set; }
        public bool IsExpert { get; set; }
    }
}
