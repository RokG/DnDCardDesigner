using CardDesigner.Domain.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CardDesigner.Domain.HelperModels
{
    public partial class AttributeModel : ObservableObject
    {
        [ObservableProperty]
        private Attribute type;

        [ObservableProperty]
        private int level;

        [ObservableProperty]
        public bool hasSavingThrows;

        [ObservableProperty]
        private int savingThrowsBonus;

        [ObservableProperty]
        private int levelBonus;

        [ObservableProperty]
        private ObservableCollection<SkillModel> skills = new();
    }
}
