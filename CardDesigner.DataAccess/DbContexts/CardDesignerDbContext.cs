using AutoMapper;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CardDesigner.DataAccess.DbContexts
{
    // Package manager console: Select DataAcess Project and type in:
    // add-migration Initial -context CardDesignerDbContext
    public class CardDesignerDbContext : DbContext
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Database context options</param>
        /// <param name="mapper">Mapper object</param>
        public CardDesignerDbContext(DbContextOptions options, IMapper mapper) : base(options)
        {
            _mapper = mapper;
        }

        // Database objects

        public DbSet<CharacterEntity> Characters { get; set; }
        public DbSet<SpellDeckDesignEntity> SpellDeckDesigns { get; set; }
        public DbSet<ItemDeckDesignEntity> ItemDeckDesigns { get; set; }
        public DbSet<CharacterDeckDesignEntity> CharacterDeckDesigns { get; set; }
        public DbSet<SpellDeckDesignLinkerEntity> SpellDeckDesignLinkers { get; set; }
        public DbSet<ItemDeckDesignLinkerEntity> ItemDeckDesignLinkers { get; set; }
        public DbSet<DeckBackgroundDesignEntity> DeckBackgroundDesigns { get; set; }
        public DbSet<SpellDeckEntity> SpellDecks { get; set; }
        public DbSet<ItemDeckEntity> ItemDecks { get; set; }
        public DbSet<SpellCardEntity> SpellCards { get; set; }
        public DbSet<ItemCardEntity> ItemCards { get; set; }
        public DbSet<CharacterCardEntity> CharacterCards { get; set; }
        public DbSet<CharacterDeckEntity> CharacterDecks { get; set; }
        public DbSet<CharacterClassEntity> CharacterClasses { get; set; }
        public DbSet<CharacterAbilitiesEntity> CharacterAbilities { get; set; }
        public DbSet<CasterStatsEntity> CasterStats { get; set; }

        //https://stackoverflow.com/questions/19342908/how-to-create-a-many-to-many-mapping-in-entity-framework

        /// <summary>
        /// Link database objects
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Character Spell deck descriptors
            modelBuilder.Entity<SpellDeckDesignLinkerEntity>()
                   .HasOne(c => c.Character)
                   .WithMany(e => e.SpellDeckDescriptors);

            // Character Item deck descriptors
            modelBuilder.Entity<ItemDeckDesignLinkerEntity>()
                   .HasOne(c => c.Character)
                   .WithMany(e => e.ItemDeckDescriptors);

            // Character Character deck descriptors
            modelBuilder.Entity<CharacterDeckDesignLinkerEntity>()
                   .HasOne(c => c.Character)
                   .WithMany(e => e.CharacterDeckDescriptors);

            // Character Classes
            modelBuilder.Entity<CharacterClassEntity>()
                   .HasOne(c => c.Character)
                   .WithMany(e => e.Classes);

            // Character Skills
            modelBuilder.Entity<CharacterAbilitiesEntity>()
                   .HasOne(c => c.Character)
                   .WithOne(e => e.Abilities)
                   .HasForeignKey<CharacterEntity>(b => b.ID);

            // Caster Stats
            modelBuilder.Entity<CasterStatsEntity>()
                   .HasOne(c => c.Character)
                   .WithOne(e => e.CasterStats)
                   .HasForeignKey<CharacterEntity>(b => b.ID);

            // Deck Background Design - Character
            modelBuilder.Entity<CharacterEntity>()
                   .HasOne(c => c.DeckBackgroundDesign)
                   .WithMany(e => e.Characters);

            // Character, Spell Deck, Deck Design
            modelBuilder.Entity<CharacterEntity>()
                   .HasMany(c => c.SpellDeckDescriptors)
                   .WithOne(e => e.Character);

            // Character, Item Deck, Deck Design
            modelBuilder.Entity<CharacterEntity>()
                   .HasMany(c => c.ItemDeckDescriptors)
                   .WithOne(e => e.Character);

            // Character, Character Deck, Deck Design
            modelBuilder.Entity<CharacterEntity>()
                   .HasMany(c => c.CharacterDeckDescriptors)
                   .WithOne(e => e.Character);

            // Spell Deck - Spell Card
            modelBuilder.Entity<SpellCardEntity>()
                .HasMany(c => c.SpellDecks)
                .WithMany(c => c.SpellCards)
                .UsingEntity<SpellDeckSpellCard>(
                    j => j
                        .HasOne(t => t.SpellDeck)
                        .WithMany(c => c.SpellDeckSpellCards)
                        .HasForeignKey(c => c.SpellDeckID),
                    j => j
                        .HasOne(t => t.SpellCard)
                        .WithMany(c => c.SpellDeckSpellCards)
                        .HasForeignKey(c => c.SpellCardID),
                    j =>
                    {
                        j.HasKey(t => new { t.SpellDeckID, t.SpellCardID });
                    });

            // Item Deck - Item Card
            modelBuilder.Entity<ItemDeckItemCard>()
                .HasKey(t => new { t.ItemCardID, t.ItemDeckID });

            modelBuilder.Entity<ItemCardEntity>()
               .HasMany(c => c.ItemDecks)
               .WithMany(c => c.ItemCards)
               .UsingEntity<ItemDeckItemCard>(
                   j => j
                       .HasOne(t => t.ItemDeck)
                       .WithMany(c => c.ItemDeckItemCards)
                       .HasForeignKey(c => c.ItemDeckID),
                   j => j
                       .HasOne(t => t.ItemCard)
                       .WithMany(c => c.ItemDeckItemCards)
                       .HasForeignKey(c => c.ItemCardID),
                   j =>
                   {
                       j.HasKey(t => new { t.ItemDeckID, t.ItemCardID });
                   });

            // Character Deck - Character Card
            modelBuilder.Entity<CharacterDeckCharacterCard>()
                .HasKey(t => new { t.CharacterCardID, t.CharacterDeckID });

            modelBuilder.Entity<CharacterCardEntity>()
               .HasMany(c => c.CharacterDecks)
               .WithMany(c => c.CharacterCards)
               .UsingEntity<CharacterDeckCharacterCard>(
                   j => j
                       .HasOne(t => t.CharacterDeck)
                       .WithMany(c => c.CharacterDeckCharacterCards)
                       .HasForeignKey(c => c.CharacterDeckID),
                   j => j
                       .HasOne(t => t.CharacterCard)
                       .WithMany(c => c.CharacterDeckCharacterCards)
                       .HasForeignKey(c => c.CharacterCardID),
                   j =>
                   {
                       j.HasKey(t => new { t.CharacterDeckID, t.CharacterCardID });
                   });

            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            //https://wildermuth.com/2018/08/12/Seeding-Related-Entities-in-EF-Core-2-1-s-HasData()/

            SeedSpellEntities(modelBuilder);

            SeedItemEntities(modelBuilder);

            SeedCharacterEntities(modelBuilder);
        }

        /// <summary>
        /// Seed character related entities
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void SeedCharacterEntities(ModelBuilder modelBuilder)
        {
            IList<CharacterCardEntity> spellCards = new List<CharacterCardEntity>
            {
                new CharacterCardEntity() {ID = 1, Name="SampleCharacterCard_1",  Type=CharacterCardType.Avatar, TitleFontSize=16, DescriptionFontSize=14 },
                new CharacterCardEntity() {ID = 2, Name="SampleCharacterCard_2", Type=CharacterCardType.Abilities, TitleFontSize=16, DescriptionFontSize=14 },
                new CharacterCardEntity() {ID = 3, Name="SampleCharacterCard_3", Type=CharacterCardType.Feats, TitleFontSize=16, DescriptionFontSize=14, Level=2, Title="Blood Tithe", Description = "Starting at level 2, you can cut your wrist to heal 2 x d4 + 2 HP. You can use this twice per short rest." },
            };
            modelBuilder.Entity<CharacterCardEntity>().HasData(spellCards);

            CharacterDeckDesignEntity characterDeckDesign = new CharacterDeckDesignEntity() { ID = 1, Name = "SampleCharacterDeckDesign_1" };
            modelBuilder.Entity<CharacterDeckDesignEntity>().HasData(characterDeckDesign);

            CharacterDeckEntity characterDeck = new CharacterDeckEntity() { ID = 1, Name = "SampleCharacterDeck_1" };
            modelBuilder.Entity<CharacterDeckEntity>().HasData(characterDeck);

            CharacterEntity character = new() { ID = 1, AvatarImageStretch = "Uniform", AvatarImagePath = "/Resources/Images/sampleimageavatar.png",  Name = "SampleCharacter_1", Weight = "100 kg", Age = "25 y", Alignment = Alignment.ChaoticNeutral, ArmourClass = 12, Height = "6 ft", Hitpoints = 40, Initiative = -1, IsHeavyArmourProficient = false, IsLightArmourProficiency = true, IsMartialWeaponProficient = true, IsMediumArmourProficient = false, IsShieldProficient = false, IsSimpleWeaponProficient = false, Proficiency = 2, OtherProficiencies = "Healing kit, Blacksmith tools", PassiveInsight = 14, PassivePerception = 12, Race = Race.HalfOrc, Speed = 25, Title = "Waltung Kremis" };
            modelBuilder.Entity<CharacterEntity>().HasData(character);

            modelBuilder.Entity<CharacterClassEntity>().HasData(new
            {
                ID = 1,
                ClassID = "class1",
                Level = 3,
                ClassSpecialization = "A",
                CharacterID = 1,
            });

            modelBuilder.Entity<CasterStatsEntity>().HasData(new
            {
                ID = 1,
                CantripsKnown = 3,
                KnownSpells = 6,
                PreparedSpells = 4,
                SpellSaveDC = 13,
                SpellAttackBonus = 3,
                SpellSlotsLevel1 = 4,
                SpellSlotsLevel2 = 2,
                SpellSlotsLevel3 = 0,
                SpellSlotsLevel4 = 0,
                SpellSlotsLevel5 = 0,
                SpellSlotsLevel6 = 0,
                SpellSlotsLevel7 = 0,
                SpellSlotsLevel8 = 0,
                SpellSlotsLevel9 = 0,
                CharacterID = 1,
            });

            modelBuilder.Entity<CharacterAbilitiesEntity>().HasData(new
            {
                ID = 1,

                Proficiency = 3,

                StrengthSavingThrows = false,
                StrengthLevel = 8,
                DexteritySavingThrows = true,
                DexterityLevel = 10,
                ConstitutionSavingThrows = false,
                ConstitutionLevel = 14,
                InteligenceSavingThrows = false,
                InteligenceLevel = 18,
                WisdomSavingThrows = true,
                WisdomLevel = 14,
                CharismaSavingThrows = false,
                CharismaLevel = 6,

                AthleticsProficiency = false,
                AthleticsExperties = false,

                AcrobaticsProficiency = false,
                AcrobaticsExperties = true,
                SleightOfHandProficiency = false,
                SleightOfHandExperties = false,
                StealthProficiency = true,
                StealthExperties = false,

                ArcanaProficiency = false,
                ArcanaExperties = false,
                HistoryProficiency = false,
                HistoryExperties = false,
                InvestigationProficiency = false,
                InvestigationExperties = false,
                NatureProficiency = false,
                NatureExperties = false,
                ReligionProficiency = false,
                ReligionExperties = false,

                AnimalHandlingProficiency = false,
                AnimalHandlingExperties = true,
                InsightProficiency = false,
                InsightExperties = false,
                MedicineProficiency = false,
                MedicineExperties = false,
                PerceptionProficiency = false,
                PerceptionExperties = false,
                SurvivalProficiency = false,
                SurvivalExperties = false,

                DeceptionProficiency = false,
                DeceptionExperties = false,
                IntimidationProficiency = true,
                IntimidationExperties = false,
                PerformanceProficiency = false,
                PerformanceExperties = false,
                PersuasionProficiency = false,
                PersuasionExperties = false,

                CharacterID = 1,
            });
        }

        /// <summary>
        /// Seed Item Card, Deck, Design entiteies
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void SeedItemEntities(ModelBuilder modelBuilder)
        {
            IList<ItemCardEntity> itemCards = new List<ItemCardEntity>
            {
                new ItemCardEntity() {ID = 1, Name="SampleItemCard_1", IconStretch="Fill", IconFilePath="/Resources/Images/sampleimage1.jpeg", Level = 1, Description="This strange armour is very hairy. Identify it to find out its properties", DescriptionFontSize=14, IsMagical = true, IsUnidentified = true, ItemID="chest1", Title="Hairy chest", RequiresAttunement=true, Type=ItemType.Armour, TitleFontSize=16},
                new ItemCardEntity() {ID = 2, Name="SampleItemCard_2", IconStretch="Fill", IconFilePath="/Resources/Images/sampleimage2.jpg",Level = 2, Description="A very common sword mostly used by nobility", DescriptionFontSize=14, IsMagical = false, IsUnidentified = false, ItemID="melee1", Title="Common Longsword", RequiresAttunement=false, Type=ItemType.Weapon, TitleFontSize=16},
                new ItemCardEntity() {ID = 3, Name="SampleItemCard_3", IconStretch="Uniform", IconFilePath="/Resources/Images/sampleimage3.png",Level = 3, Description="Special bow designed by the best dwarven engineers. Add +1d6 on successful hit", DescriptionFontSize=14, IsMagical = true, IsUnidentified = false, ItemID="ranged1", Title="Big Bertha", RequiresAttunement=true, Type=ItemType.Weapon, TitleFontSize=16},
            };
            modelBuilder.Entity<ItemCardEntity>().HasData(itemCards);

            ItemDeckDesignEntity itemDeckDesign = new ItemDeckDesignEntity() { ID=1, Name = "SampleItemDeckDesign_1"};
            modelBuilder.Entity<ItemDeckDesignEntity>().HasData(itemDeckDesign);

            ItemDeckEntity itemDeck = new ItemDeckEntity() { ID = 1, Name = "SampleItemDeck_1" };
            modelBuilder.Entity<ItemDeckEntity>().HasData(itemDeck);
        }

        /// <summary>
        /// Seed Spell Card, Deck, Design entiteies
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void SeedSpellEntities(ModelBuilder modelBuilder)
        {
            IList<SpellCardEntity> spellCards = new List<SpellCardEntity>
            {
                new SpellCardEntity() {ID = 1, Name="SampleSpellCard_1", Level = 0, IsRitual=true, IsConcentration = false, AreaOfEffect=AreaOfEffect.Sphere, AreaOfEffectValue=10, CastingTimeType=CastingTimeType.Action, CastingTimeValue=1, DamageType=MagicDamageType.Radiant, Description="Casts something", DescriptionFontSize=14, DiceType=DiceType.d8, DiceValue = 4, DurationType = DurationType.Instantaneous, DurationValue=0, HasMaterialComponent=true, HasSomaticComponent=true, HasVerbalComponent =true, RangeType=RangeType.Distance, RangeValue=60, School=MagicSchool.Evocation, Target="Humanoid Within range", TargetType=TargetType.Target, Title="Spell Of Knowledge", TitleFontSize=16},
                new SpellCardEntity() {ID = 2, Name="SampleSpellCard_2", Level = 3, IsRitual=false, IsConcentration = true, AreaOfEffect=AreaOfEffect.Line, AreaOfEffectValue=30, CastingTimeType=CastingTimeType.BonusAction, CastingTimeValue=1, DamageType=MagicDamageType.Fire, Description="Enchant yourself", DescriptionFontSize=18, DiceType=DiceType.d4, DiceValue = 1, DurationType = DurationType.Minute, DurationValue=10, HasMaterialComponent=false, HasSomaticComponent=true, HasVerbalComponent =false, RangeType=RangeType.Self, RangeValue=0, School=MagicSchool.Divination, Target="Self", TargetType=TargetType.Self, Title="Aura of vitality", TitleFontSize=18},
                new SpellCardEntity() {ID = 3, Name="SampleSpellCard_3", Level = 7, IsRitual=true, IsConcentration = false, AreaOfEffect=AreaOfEffect.Sphere, AreaOfEffectValue=10, CastingTimeType=CastingTimeType.Minute, CastingTimeValue=10, DamageType=MagicDamageType.None, Description="Make a table within range fight for you", DescriptionFontSize=14, DiceType=DiceType.d20, DiceValue = 3, DurationType = DurationType.Instantaneous, DurationValue=0, HasMaterialComponent=false, HasSomaticComponent=true, HasVerbalComponent =true, RangeType=RangeType.Touch, RangeValue=0, School=MagicSchool.Necromancy, Target="Object you can touch", TargetType=TargetType.Touch, Title="Raise Tables", TitleFontSize=14},
            };
            modelBuilder.Entity<SpellCardEntity>().HasData(spellCards);

            SpellDeckDesignEntity spellDeckDesign = new SpellDeckDesignEntity() { ID = 1, Name = "SampleSpellDeckDesign_1" };
            modelBuilder.Entity<SpellDeckDesignEntity>().HasData(spellDeckDesign);

            SpellDeckEntity spellDeck = new SpellDeckEntity() { ID = 1, Name = "SampleSpellDeck_1" };
            modelBuilder.Entity<SpellDeckEntity>().HasData(spellDeck);
        }
    }
}