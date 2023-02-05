using CardDesigner.Domain.Enums;
using System;

namespace CardDesigner.Domain.HelperModels
{
    public class SkillModel
    {
        public Skill Type { get; set; }
        public bool IsExpert { get; set; }
        public bool IsProficient { get; set; }
        public bool IsBasic { get; set; }
    }
}
