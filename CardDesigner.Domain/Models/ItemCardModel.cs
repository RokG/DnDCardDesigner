using CardDesigner.Domain.Enums;
using CardDesigner.Domain.Interfaces;

namespace CardDesigner.Domain.Models
{
    public class ItemCardModel : ICard, IItem, ISelectableItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; } = 0;
        public EquipmentSlot EquipmentSlotSlot { get; set; }
        public PhysicalDamageType PhysicalDamageType { get; set; }
        public MagicDamageType MagicDamageType { get; set; }
        public int ArmourClass { get; set; }
        public int DamageValue { get; set; }
        public int DamageModifier { get; set; }
        public ItemType Type { get; set; }
        public bool IsUnidentified { get; set; }
        public bool RequiresAttunement { get; set; }
        public bool IsMagical { get; set; }
    }
}