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
        public DbSet<CardDesignEntity> CardDesigns { get; set; }
        public DbSet<SpellDeckDesignEntity> SpellDeckDesigns { get; set; }
        public DbSet<ItemDeckDesignEntity> ItemDeckDesigns { get; set; }
        public DbSet<CardDesignEntity> CharacterDeckDesigns { get; set; }
        public DbSet<SpellDeckEntity> SpellDecks { get; set; }
        public DbSet<ItemDeckEntity> ItemDecks { get; set; }
        public DbSet<SpellCardEntity> SpellCards { get; set; }
        public DbSet<ItemCardEntity> ItemCards { get; set; }

        //https://stackoverflow.com/questions/19342908/how-to-create-a-many-to-many-mapping-in-entity-framework

        /// <summary>
        /// Link database objects
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Character Spell Deck - Deck design
            modelBuilder.Entity<CharacterEntity>()
                   .HasOne(c => c.DeckBackgroundDesign)
                   .WithMany(e => e.Characters);

            // Character Spell Deck - Deck design
            modelBuilder.Entity<CharacterEntity>()
                   .HasMany(c => c.SpellDeckDescriptors)
                   .WithOne(e => e.Character);

            // Character Item Deck - Deck design
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
        }
    }
}