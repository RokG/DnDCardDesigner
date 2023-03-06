namespace CardDesigner.Domain.Models
{
    public class CasterStatsModel
    {
        // Entity links
        public int ID { get; set; }

        // Properties
        public int SpellAttackBonus { get; set; }
        public int SpellSaveDC { get; set; }
        public int KnownSpells { get; set; }
        public int PreparedSpells { get; set; }
        public int CantripsKnown { get; set; }
        public int SpellSlotsLevel1 { get; set; }
        public int SpellSlotsLevel2 { get; set; }
        public int SpellSlotsLevel3 { get; set; }
        public int SpellSlotsLevel4 { get; set; }
        public int SpellSlotsLevel5 { get; set; }
        public int SpellSlotsLevel6 { get; set; }
        public int SpellSlotsLevel7 { get; set; }
        public int SpellSlotsLevel8 { get; set; }
        public int SpellSlotsLevel9 { get; set; }
    }
}
