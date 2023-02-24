using CardDesigner.Domain.Enums;
using System.Collections.Generic;

namespace CardDesigner.Domain.HelperModels
{
    public class ClassModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public List<string> Specializations { get; set; }
        public DiceType HitDice { get; set; }
    }
}
