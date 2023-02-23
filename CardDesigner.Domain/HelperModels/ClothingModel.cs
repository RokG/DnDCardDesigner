using CardDesigner.Domain.Enums;

namespace CardDesigner.Domain.HelperModels
{
    public class ClothingModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public ArmourType ArmourType { get; set; }
        public EquipmentSlot EquipmentSlot { get; set; }
    }
}
