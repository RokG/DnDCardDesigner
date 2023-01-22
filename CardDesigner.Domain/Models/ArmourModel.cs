using CardDesigner.Domain.Enums;

namespace CardDesigner.Domain.Models
{
    public class ArmourModel : IItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public ArmourType ArmourType { get; set; }
        public EquipmentSlot EquipmentSlot { get; set; }
        public int ArmourClass { get; set; }
    }
}
