using CardDesigner.Domain.Enums;

namespace CardDesigner.Domain.HelperModels
{
    public class SkillModel
    {
        public Skill Type { get; set; }
        public bool IsExpert { get; set; }
        public bool IsProficient { get; set; }
        public bool IsBasic { get; set; }
        public int Bonus { get; set; }
    }
}
