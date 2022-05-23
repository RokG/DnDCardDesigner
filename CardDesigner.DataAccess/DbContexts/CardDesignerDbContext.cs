using AutoMapper;
using CardDesigner.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardDesigner.DataAccess.DbContexts
{
    public class CardDesignerDbContext : DbContext
    {
        private readonly IMapper _mapper;

        public CardDesignerDbContext(DbContextOptions options, IMapper mapper) : base(options)
        {
            _mapper = mapper;
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<CardDeck> CardDecks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>()
                .HasMany(c => c.Decks)
                .WithOne(c => c.Character)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CardDeck>()
               .HasMany(c => c.SpellCards)
               .WithOne(c => c.Deck)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}