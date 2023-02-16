using CardDesigner.Domain.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CardDesigner.Domain.HelperModels
{
    public partial class SkillModel : ObservableObject
    {
        [ObservableProperty]
        private Skill type;

        [ObservableProperty]
        private bool isExpert;

        [ObservableProperty]
        private bool isProficient;

        [ObservableProperty]
        private bool isBasic;

        [ObservableProperty]
        private int bonus;
    }
}
