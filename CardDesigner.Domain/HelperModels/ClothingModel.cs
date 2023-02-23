using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;

namespace CardDesigner.Domain.HelperModels
{
    public class ClothingModel : IItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public ArmourType ArmourType { get; set; }
        public EquipmentSlot EquipmentSlot { get; set; }
    }
}
