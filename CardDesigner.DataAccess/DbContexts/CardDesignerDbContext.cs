using AutoMapper;
using CardDesigner.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
            // Character Classes
            modelBuilder.Entity<SpellDeckDesignLinkerEntity>()
                   .HasOne(c => c.Character)
                   .WithMany(e => e.SpellDeckDescriptors);

            // Character Classes
            modelBuilder.Entity<ItemDeckDesignLinkerEntity>()
                   .HasOne(c => c.Character)
                   .WithMany(e => e.ItemDeckDescriptors);

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

            // Deck Background Design - Character
            modelBuilder.Entity<CharacterEntity>()
                   .HasOne(c => c.CharacterDeckDesign)
                   .WithMany(e => e.Characters);

            // Character, Spell Deck, Deck Design
            modelBuilder.Entity<CharacterEntity>()
                   .HasMany(c => c.SpellDeckDescriptors)
                   .WithOne(e => e.Character);

            // Character, Item Deck, Deck Design
            modelBuilder.Entity<CharacterEntity>()
                   .HasMany(c => c.ItemDeckDescriptors)
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
        }
    }
}