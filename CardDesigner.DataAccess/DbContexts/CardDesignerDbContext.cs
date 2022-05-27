using AutoMapper;
using CardDesigner.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardDesigner.DataAccess.DbContexts
{
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

        public DbSet<Character> Characters { get; set; }
        public DbSet<SpellDeck> SpellDecks { get; set; }
        public DbSet<SpellCard> SpellCards { get; set; }

        //https://stackoverflow.com/questions/19342908/how-to-create-a-many-to-many-mapping-in-entity-framework

        /// <summary>
        /// Link database objects
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>()
                .HasMany(c => c.SpellDecks)
                .WithOne(c => c.Character)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SpellDeck>()
                .HasOne(c => c.Character)
                .WithMany(c => c.SpellDecks)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SpellDeckSpellCard>()
                .HasKey(t => new { t.SpellCardID, t.SpellDeckID });

            //modelBuilder.Entity<SpellCardSpellDeck>()
            //.HasOne(pt => pt.SpellDeck)
            //.WithMany(p => p.DeckSpellCards)
            //.HasForeignKey(pt => pt.SpellDeckID);

            //modelBuilder.Entity<SpellCardSpellDeck>()
            //.HasOne(pt => pt.SpellCard)
            //.WithMany(p => p.DeckSpellCards)
            //.HasForeignKey(pt => pt.SpellCardID);

            modelBuilder.Entity<SpellCard>()
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



            //modelBuilder.Entity<SpellDeck>()
            //    .HasMany(c => c.SpellCards)
            //    .WithMany(c => c.SpellDecks)
            //    .UsingEntity<SpellDeckSpellCard>(
            //        j => j
            //            .HasOne(t => t.SpellCard)
            //            .WithMany(c => c.SpellDeckSpellCards)
            //            .HasForeignKey(c => c.SpellCardID),
            //        j => j
            //            .HasOne(t => t.SpellDeck)
            //            .WithMany(c => c.SpellDeckSpellCards)
            //            .HasForeignKey(c => c.SpellDeckID),
            //        j =>
            //            {
            //                j.HasKey(t => new { t.SpellCardID });
            //            });
        }
    }
}