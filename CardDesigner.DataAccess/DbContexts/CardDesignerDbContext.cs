﻿using AutoMapper;
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
            modelBuilder.Entity<CharacterEntity>()
                .HasOne(c => c.SpellDeck)
                .WithMany(c => c.Characters)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SpellDeckEntity>()
                .HasMany(c => c.Characters)
                .WithOne(c => c.SpellDeck)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SpellDeckSpellCard>()
                .HasKey(t => new { t.SpellCardID, t.SpellDeckID });

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

            modelBuilder.Entity<CharacterEntity>()
                .HasOne(c => c.ItemDeck)
                .WithMany(c => c.Characters)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemDeckEntity>()
                .HasMany(c => c.Characters)
                .WithOne(c => c.ItemDeck)
                .OnDelete(DeleteBehavior.Restrict);

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