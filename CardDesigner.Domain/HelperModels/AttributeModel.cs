using CardDesigner.Domain.Enums;
using System.Collections.Generic;

namespace CardDesigner.Domain.HelperModels
{
    public class AttributeModel
    {
        public Attribute Type { get; set; }
        public int Level { get; set; }
        public bool SavingThrows { get; set; }
        public List<SkillModel> Skills { get; set; }
    }
}
