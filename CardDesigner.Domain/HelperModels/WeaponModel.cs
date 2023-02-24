using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Models;

namespace CardDesigner.Domain.HelperModels
{
    public class WeaponModel : IItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public WeaponType WeaponType { get; set; }
        public EquipmentSlot EquipmentSlot { get; set; }
        public int DiceValue { get; set; } = 0;
        public DiceType DiceType { get; set; } = DiceType.d4;
        public PhysicalDamageType PhysicalDamageType { get; set; }
        public MagicDamageType MagicDamageType { get; set; }
        public int DamageModifier { get; set; }
    }
}
