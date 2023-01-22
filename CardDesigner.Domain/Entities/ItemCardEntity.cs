using CardDesigner.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardDesigner.Domain.Entities
{
    public class ItemCardEntity
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public ICollection<ItemDeckEntity> ItemDecks { get; set; }
        public List<ItemDeckItemCard> ItemDeckItemCards { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public string IconFilePath { get; set; }
        public string IconStretch { get; set; }
        public int Level { get; set; }
        public EquipmentSlot EquipmentSlotSlot { get; set; }
        public PhysicalDamageType PhysicalDamageType { get; set; }
        public MagicDamageType MagicDamageType { get; set; }
        public int ArmourClass { get; set; }
        public int DamageValue { get; set; }
        public int DamageModifier { get; set; }
        public bool IsUnidentified { get; set; }
        public bool RequiresAttunement { get; set; }
        public bool IsMagical { get; set; }
    }
}