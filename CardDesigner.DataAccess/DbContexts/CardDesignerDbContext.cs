﻿using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CardDesigner.DataAccess.DbContexts
{
    // Package manager console: Select DataAcess Project and type in:
    // add-migration Initial -context CardDesignerDbContext
    public class CardDesignerDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Database context options</param>
        /// <param name="mapper">Mapper object</param>
        public CardDesignerDbContext(DbContextOptions options) : base(options)
        {
        }

        #region Database objects

        // Character
        public DbSet<CharacterEntity> Characters { get; set; }
        public DbSet<DeckBackgroundDesignEntity> DeckBackgroundDesigns { get; set; }
        public DbSet<CharacterClassEntity> CharacterClasses { get; set; }
        public DbSet<CharacterAbilitiesEntity> CharacterAbilities { get; set; }
        public DbSet<CasterStatsEntity> CasterStats { get; set; }

        // Minions
        public DbSet<MinionEntity> Minions { get; set; }

        // Spell cards
        public DbSet<SpellCardEntity> SpellCards { get; set; }
        public DbSet<SpellDeckEntity> SpellDecks { get; set; }
        public DbSet<SpellDeckDesignEntity> SpellDeckDesigns { get; set; }
        public DbSet<SpellDeckDesignLinkerEntity> SpellDeckDesignLinkers { get; set; }

        // Item cards
        public DbSet<ItemCardEntity> ItemCards { get; set; }
        public DbSet<ItemDeckEntity> ItemDecks { get; set; }
        public DbSet<ItemDeckDesignEntity> ItemDeckDesigns { get; set; }
        public DbSet<ItemDeckDesignLinkerEntity> ItemDeckDesignLinkers { get; set; }

        // Character cards
        public DbSet<CharacterCardEntity> CharacterCards { get; set; }
        public DbSet<CharacterDeckEntity> CharacterDecks { get; set; }
        public DbSet<CharacterDeckDesignEntity> CharacterDeckDesigns { get; set; }
        public DbSet<CharacterDeckDesignLinkerEntity> CharacterDeckDesignLinkers { get; set; }

        // Minion cards
        public DbSet<MinionCardEntity> MinionCards { get; set; }
        public DbSet<MinionDeckEntity> MinionDecks { get; set; }
        public DbSet<MinionDeckDesignEntity> MinionDeckDesigns { get; set; }
        public DbSet<MinionDeckDesignLinkerEntity> MinionDeckDesignLinkers { get; set; }

        #endregion

        //https://stackoverflow.com/questions/19342908/how-to-create-a-many-to-many-mapping-in-entity-framework

        /// <summary>
        /// Link database objects
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Base

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

            // Minion - Minion card
            modelBuilder.Entity<MinionEntity>()
                .HasMany(c => c.MinionCards)
                .WithOne(e => e.Minion);

            #endregion

            #region Deck descriptors

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

            // Character Minion deck descriptors
            modelBuilder.Entity<MinionDeckDesignLinkerEntity>()
                   .HasOne(c => c.Character)
                   .WithMany(e => e.MinionDeckDescriptors);

            #endregion

            #region Designs

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

            // Character, Minion Deck, Deck Design
            modelBuilder.Entity<CharacterEntity>()
                   .HasMany(c => c.MinionDeckDescriptors)
                   .WithOne(e => e.Character);

            // Character, Character Deck, Deck Design
            modelBuilder.Entity<CharacterEntity>()
                   .HasMany(c => c.CharacterDeckDescriptors)
                   .WithOne(e => e.Character);

            #endregion

            #region Deck-Cards

            // Spell Deck - Spell Card
            modelBuilder.Entity<SpellCardEntity>()
                .HasMany(c => c.SpellDecks)
                .WithMany(c => c.Cards)
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
               .WithMany(c => c.Cards)
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
               .WithMany(c => c.Cards)
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

            // Minion Deck - Minion Card
            modelBuilder.Entity<MinionDeckMinionCard>()
                .HasKey(t => new { t.MinionCardID, t.MinionDeckID });

            modelBuilder.Entity<MinionCardEntity>()
               .HasMany(c => c.MinionDecks)
               .WithMany(c => c.Cards)
               .UsingEntity<MinionDeckMinionCard>(
                   j => j
                       .HasOne(t => t.MinionDeck)
                       .WithMany(c => c.MinionDeckMinionCards)
                       .HasForeignKey(c => c.MinionDeckID),
                   j => j
                       .HasOne(t => t.MinionCard)
                       .WithMany(c => c.MinionDeckMinionCards)
                       .HasForeignKey(c => c.MinionCardID),
                   j =>
                   {
                       j.HasKey(t => new { t.MinionDeckID, t.MinionCardID });
                   });

            #endregion

            //Seed(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            //https://wildermuth.com/2018/08/12/Seeding-Related-Entities-in-EF-Core-2-1-s-HasData()/

            SeedSpellEntities(modelBuilder);

            SeedItemEntities(modelBuilder);

            SeedCharacterEntities(modelBuilder);

            SeedMinionEntities(modelBuilder);
        }

        private static void SeedMinionEntities(ModelBuilder modelBuilder)
        {
            MinionEntity minion = new()
            {
                ID = 1,
                Name = "SampleMinion_1",
                Title = "Abaraton",
                Strength = 10,
                Dexterity = 10,
                Constitution = 10,
                Inteligence = 10,
                Wisdom = 10,
                Charisma = 10,
                ChalangeRating = "1/4",
                ArmourClass = 12,
                Actions = "Can do shit",
                LegendaryActions = "Can do better shit",
                Alignment = Alignment.ChaoticGood,
                Appearance = "Small Humanoid",
                Attributes = "Can walk on water like Jesus",
                ClimbingSpeed = 30,
                SwimingSpeed = 20,
                Speed = 20,
                FlyingSpeed = 30,
                ConditionImmunities = "Blinded, Cursed",
                DamageImmunities = "Blunt",
                DamageResistances = "Fire",
                PassivePerception = 12,
                Hitpoints = 80,
                Initiative = +1,
                Languages = "archaic",
                SavingThrows = "Strength, Dexterity",
                Senses = "Keen senses",
                SkillBonuses = "+1 Animal handling"
            };
            modelBuilder.Entity<MinionEntity>().HasData(minion);

            modelBuilder.Entity<MinionCardEntity>().HasData(
                new { ID = 1, Name = "SampleMinionCard_1", Title = "Abaraton - Stats", MinionID = minion.ID, Type = MinionCardType.Stats, DescriptionFontSize = 18.0, TitleFontSize = 14.0 },
                new { ID = 2, Name = "SampleMinionCard_2", Title = "Abaraton - Actions", MinionID = minion.ID, Type = MinionCardType.Actions, DescriptionFontSize = 18.0, TitleFontSize = 14.0 },
                new { ID = 3, Name = "SampleMinionCard_3", Title = "Abaraton - Attributes", MinionID = minion.ID, Type = MinionCardType.Attributes, DescriptionFontSize = 18.0, TitleFontSize = 14.0 }
            );

            MinionDeckDesignEntity minionDeckDesign = new() { ID = 1, Name = "SampleMinionDeckDesign_1" };
            modelBuilder.Entity<MinionDeckDesignEntity>().HasData(minionDeckDesign);

            MinionDeckEntity minionDeck = new() { ID = 1, Name = "SampleMinionDeck_1", Title = "Sample Minion Deck" };
            modelBuilder.Entity<MinionDeckEntity>().HasData(minionDeck);

            modelBuilder.Entity<MinionDeckDesignLinkerEntity>().HasData(new
            {
                ID = 1,
                MinionDeckID = 1,
                DesignID = 1,
                CharacterID = 1,
            });

            modelBuilder.Entity<MinionDeckEntity>().HasMany(p => p.Cards).WithMany(p => p.MinionDecks)
            .UsingEntity(j => j
            .ToTable("MinionDeckMinionCard")
            .HasData(new[]
            {
                new { MinionCardID = 2, MinionDeckID = 1},
                new { MinionCardID = 3, MinionDeckID = 1},
            }));
        }

        /// <summary>
        /// Seed character related entities
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void SeedCharacterEntities(ModelBuilder modelBuilder)
        {
            IList<CharacterCardEntity> characterCards = new List<CharacterCardEntity>
            {
                new CharacterCardEntity() {ID = 1, Name="SampleCharacterCard_1", Title = "Waltung Kremis - Avatar", Type=CharacterCardType.Avatar, TitleFontSize=16, DescriptionFontSize=14 },
                new CharacterCardEntity() {ID = 2, Name="SampleCharacterCard_2", Title = "Waltung Kremis - Abilities",Type=CharacterCardType.Abilities, TitleFontSize=16, DescriptionFontSize=14 },
                new CharacterCardEntity() {ID = 3, Name="SampleCharacterCard_3", Type=CharacterCardType.Feats, TitleFontSize=16, DescriptionFontSize=14, Level=2, Title="Blood Tithe", Description = "Starting at level 2, you can cut your wrist to heal 2 x d4 + 2 HP. You can use this twice per short rest." },
            };
            modelBuilder.Entity<CharacterCardEntity>().HasData(characterCards);

            CharacterDeckDesignEntity characterDeckDesign = new() { ID = 1, Name = "SampleCharacterDeckDesign_1" };

            IList<CharacterDeckDesignEntity> characterDeckDesigns = new List<CharacterDeckDesignEntity>
            {
                 new() { ID = 1, Name = "SampleBackgroundDesign_1" },
                 new() { ID = 2, Name = "SampleBackgroundDesign_2" },
            };
            modelBuilder.Entity<CharacterDeckDesignEntity>().HasData(characterDeckDesigns);

            DeckBackgroundDesignEntity deckBackgroundDesign = new() { ID = 1, Name = "SampleBackgroundDesign_1" };
            modelBuilder.Entity<DeckBackgroundDesignEntity>().HasData(deckBackgroundDesign);

            IList<CharacterDeckEntity> characterDecks = new List<CharacterDeckEntity>
            {
                 new CharacterDeckEntity() { ID = 1, Name = "SampleCharacterDeck_1", Title = "Sample Character Deck 1" },
                 new CharacterDeckEntity() { ID = 2, Name = "SampleCharacterDeck_2", Title = "Sample Character Deck 2" },
            };
            modelBuilder.Entity<CharacterDeckEntity>().HasData(characterDecks);

            modelBuilder.Entity<CharacterDeckDesignLinkerEntity>().HasData(new
            {
                ID = 1,
                CharacterDeckID = 1,
                DesignID = 1,
                CharacterID = 1,
            });
            modelBuilder.Entity<CharacterDeckDesignLinkerEntity>().HasData(new
            {
                ID = 2,
                CharacterDeckID = 2,
                DesignID = 2,
                CharacterID = 1,
            });

            modelBuilder.Entity<CharacterDeckEntity>().HasMany(p => p.Cards).WithMany(p => p.CharacterDecks)
            .UsingEntity(j => j
            .ToTable("CharacterDeckCharacterCard")
            .HasData(new[]
            {
                new { CharacterCardID = 1, CharacterDeckID = 1},
                new { CharacterCardID = 3, CharacterDeckID = 1},
            }));

            modelBuilder.Entity<CharacterDeckEntity>().HasMany(p => p.Cards).WithMany(p => p.CharacterDecks)
           .UsingEntity(j => j
           .ToTable("CharacterDeckCharacterCard")
           .HasData(new[]
           {
                new { CharacterCardID = 2, CharacterDeckID = 2},
                new { CharacterCardID = 3, CharacterDeckID = 2},
           }));

            CharacterEntity character = new() { ID = 1, AvatarImagePath = "/Resources/Images/sampleimageavatar.png", Name = "SampleCharacter_1", Weight = "100 kg", Age = "25 y", Alignment = Alignment.ChaoticNeutral, ArmourClass = 12, Height = "6 ft", Hitpoints = 40, Initiative = -1, IsHeavyArmourProficient = false, IsLightArmourProficiency = true, IsMartialWeaponProficient = true, IsMediumArmourProficient = false, IsShieldProficient = false, IsSimpleWeaponProficient = false, Proficiency = 2, OtherProficiencies = "Healing kit, Blacksmith tools", PassiveInsight = 14, PassivePerception = 12, Race = Race.HalfOrc, Speed = 25, Title = "Waltung Kremis" };
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
                new ItemCardEntity() {ID = 1, Name="SampleItemCard_1", IconStretch="Fill", IconFilePath="/Resources/Images/sampleimage1.jpeg", Level = 1, Description="This strange armour is very hairy. Identify it to find out its properties", DescriptionFontSize=14.1, IsMagical = true, IsUnidentified = true, ItemID="chest1", Title="Hairy chest", RequiresAttunement=true, Type=ItemType.Armour, TitleFontSize=16},
                new ItemCardEntity() {ID = 2, Name="SampleItemCard_2", IconStretch="Fill", IconFilePath="/Resources/Images/sampleimage2.jpg",Level = 2, Description="A very common sword mostly used by nobility", DescriptionFontSize=14.1, IsMagical = false, IsUnidentified = false, ItemID="melee1", Title="Common Longsword", RequiresAttunement=false, Type=ItemType.Weapon, TitleFontSize=16},
                new ItemCardEntity() {ID = 3, Name="SampleItemCard_3", IconStretch="Uniform", IconFilePath="/Resources/Images/sampleimage3.png",Level = 3, Description="Special bow designed by the best dwarven engineers. Add +1d6 on successful hit", DescriptionFontSize=14.1, IsMagical = true, IsUnidentified = false, ItemID="ranged1", Title="Big Bertha", RequiresAttunement=true, Type=ItemType.Weapon, TitleFontSize=16},
                new ItemCardEntity() {ID = 4, Name="SampleItemCard_4", IconStretch="Uniform", IconFilePath="/Resources/Images/sampleimage3.png",Level = 3, Description="Special bow designed by the best dwarven engineers. Add +1d6 on successful hit", DescriptionFontSize=14.1, IsMagical = true, IsUnidentified = false, ItemID="ranged1", Title="Test Card 1", RequiresAttunement=true, Type=ItemType.Weapon, TitleFontSize=16},
                new ItemCardEntity() {ID = 5, Name="SampleItemCard_5", IconStretch="Uniform", IconFilePath="/Resources/Images/sampleimage3.png",Level = 3, Description="Special bow designed by the best dwarven engineers. Add +1d6 on successful hit", DescriptionFontSize=14.1, IsMagical = true, IsUnidentified = false, ItemID="ranged1", Title="Test Card 2", RequiresAttunement=true, Type=ItemType.Weapon, TitleFontSize=16},
                new ItemCardEntity() {ID = 6, Name="SampleItemCard_6", IconStretch="Uniform", IconFilePath="/Resources/Images/sampleimage3.png",Level = 3, Description="Special bow designed by the best dwarven engineers. Add +1d6 on successful hit", DescriptionFontSize=14.1, IsMagical = true, IsUnidentified = false, ItemID="ranged1", Title="Test Card 3", RequiresAttunement=true, Type=ItemType.Weapon, TitleFontSize=16},
                new ItemCardEntity() {ID = 7, Name="SampleItemCard_7", IconStretch="Uniform", IconFilePath="/Resources/Images/sampleimage3.png",Level = 3, Description="Special bow designed by the best dwarven engineers. Add +1d6 on successful hit", DescriptionFontSize=14.1, IsMagical = true, IsUnidentified = false, ItemID="ranged1", Title="Test Card 4", RequiresAttunement=true, Type=ItemType.Weapon, TitleFontSize=16},
                new ItemCardEntity() {ID = 8, Name="SampleItemCard_8", IconStretch="Uniform", IconFilePath="/Resources/Images/sampleimage3.png",Level = 3, Description="Special bow designed by the best dwarven engineers. Add +1d6 on successful hit", DescriptionFontSize=14.1, IsMagical = true, IsUnidentified = false, ItemID="ranged1", Title="Test Card 5", RequiresAttunement=true, Type=ItemType.Weapon, TitleFontSize=16},
                new ItemCardEntity() {ID = 9, Name="SampleItemCard_9", IconStretch="Uniform", IconFilePath="/Resources/Images/sampleimage3.png",Level = 3, Description="Special bow designed by the best dwarven engineers. Add +1d6 on successful hit", DescriptionFontSize=14.1, IsMagical = true, IsUnidentified = false, ItemID="ranged1", Title="Test Card 6", RequiresAttunement=true, Type=ItemType.Weapon, TitleFontSize=16},
                new ItemCardEntity() {ID = 10, Name="SampleItemCard_10", IconStretch="Uniform", IconFilePath="/Resources/Images/sampleimage3.png",Level = 3, Description="Special bow designed by the best dwarven engineers. Add +1d6 on successful hit", DescriptionFontSize=14.1, IsMagical = true, IsUnidentified = false, ItemID="ranged1", Title="Test Card 7", RequiresAttunement=true, Type=ItemType.Weapon, TitleFontSize=16},
            };
            modelBuilder.Entity<ItemCardEntity>().HasData(itemCards);

            ItemDeckDesignEntity itemDeckDesign = new() { ID = 1, Name = "SampleItemDeckDesign_1" };
            modelBuilder.Entity<ItemDeckDesignEntity>().HasData(itemDeckDesign);

            ItemDeckEntity itemDeck = new() { ID = 1, Name = "SampleItemDeck_1", Title = "Sample Item Deck" };
            IList<ItemDeckEntity> itemDecks = new List<ItemDeckEntity>
            {
                new ItemDeckEntity() { ID = 1, Name = "SampleItemDeck_1", Title = "Sample Item Deck 1" },
                new ItemDeckEntity() { ID = 2, Name = "SampleItemDeck_2", Title = "Sample Item Deck 2" },
                new ItemDeckEntity() { ID = 3, Name = "SampleItemDeck_3", Title = "Sample Item Deck 3" }
            };
            modelBuilder.Entity<ItemDeckEntity>().HasData(itemDecks);

            modelBuilder.Entity<ItemDeckDesignLinkerEntity>().HasData(new
            {
                ID = 1,
                ItemDeckID = 1,
                DesignID = 1,
                CharacterID = 1,
            });

            modelBuilder.Entity<ItemDeckEntity>().HasMany(p => p.Cards).WithMany(p => p.ItemDecks)
            .UsingEntity(j => j
            .ToTable("ItemDeckItemCard")
            .HasData(new[]
            {
                new { ItemCardID = 1, ItemDeckID = 1},
                new { ItemCardID = 2, ItemDeckID = 1},
                new { ItemCardID = 3, ItemDeckID = 1},
                new { ItemCardID = 4, ItemDeckID = 1},
                new { ItemCardID = 5, ItemDeckID = 1},
                new { ItemCardID = 6, ItemDeckID = 1},
                new { ItemCardID = 7, ItemDeckID = 1},
                new { ItemCardID = 8, ItemDeckID = 2},
                new { ItemCardID = 9, ItemDeckID = 2},
                new { ItemCardID = 10, ItemDeckID = 2},
                new { ItemCardID = 1, ItemDeckID = 2},
                new { ItemCardID = 2, ItemDeckID = 3},
                new { ItemCardID = 3, ItemDeckID = 3},
            }));
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

            SpellDeckDesignEntity spellDeckDesign = new() { ID = 1, Name = "SampleSpellDeckDesign_1" };
            modelBuilder.Entity<SpellDeckDesignEntity>().HasData(spellDeckDesign);

            SpellDeckEntity spellDeck = new() { ID = 1, Name = "SampleSpellDeck_1", Title = "Sample Spell Deck" };
            modelBuilder.Entity<SpellDeckEntity>().HasData(spellDeck);

            modelBuilder.Entity<SpellDeckDesignLinkerEntity>().HasData(new
            {
                ID = 1,
                SpellDeckID = 1,
                DesignID = 1,
                CharacterID = 1,
            });

            modelBuilder.Entity<SpellDeckEntity>().HasMany(p => p.Cards).WithMany(p => p.SpellDecks)
            .UsingEntity(j => j
            .ToTable("SpellDeckSpellCard")
            .HasData(new[]
            {
                new { SpellCardID = 2, SpellDeckID = 1},
                new { SpellCardID = 3, SpellDeckID = 1},
            }));
        }
    }
}