using CardDesigner.Domain.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace CardDesigner.Domain.HelperModels
{
    public partial class AttributeModel : ObservableObject
    {
        public Attribute Type { get; set; }
        public int Level { get; set; }
        public bool HasSavingThrows { get; set; }

        [ObservableProperty]
        private int savingThrowsBonus;
        public int LevelBonus => (Level - 10) / 2;
        public List<SkillModel> Skills { get; set; }
    }
}
