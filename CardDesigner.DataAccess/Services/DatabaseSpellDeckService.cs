using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services
{
    public class DatabaseSpellDeckService : ISpellDeckService
    {
        private readonly CardDesignerDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public DatabaseSpellDeckService(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<SpellDeckModel> CreateSpellDeck(SpellDeckModel spellDeck)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                SpellDeckEntity spellDeckEntity = _mapper.Map<SpellDeckEntity>(spellDeck);

                SpellDeckEntity createdSpellDeckEntity = dbContext.SpellDecks.Add(spellDeckEntity).Entity;
                await dbContext.SaveChangesAsync();

                return _mapper.Map<SpellDeckModel>(createdSpellDeckEntity);
            }
        }

        public async Task<SpellDeckModel> UpdateSpellDeck(SpellDeckModel spellDeck)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    // Get spell deck from database
                    SpellDeckEntity spellDeckEntity = dbContext.SpellDecks
                        .Include(sd => sd.SpellCards)
                        .Single(sc => sc.ID == spellDeck.ID);

                    // Loop over cards in source deck
                    foreach (SpellCardModel spellCard in spellDeck.SpellCards)
                    {
                        // If any card is new, add it to the list
                        if (!spellDeckEntity.SpellCards.Where(sd => sd.ID == spellCard.ID).Any())
                        {
                            SpellCardEntity spellCardEntity = _mapper.Map<SpellCardEntity>(spellCard);
                            spellDeckEntity.SpellCards.Add(spellCardEntity);
                        }
                    }

                    // Loop over cards in source deck
                    foreach (SpellCardEntity spellCard in spellDeckEntity.SpellCards)
                    {
                        // If any card is missing, remove it from the list
                        if (!spellDeck.SpellCards.Any(id => id.ID == spellCard.ID))
                        {
                            SpellCardEntity spellCardEntity = _mapper.Map<SpellCardEntity>(spellCard);
                            spellDeckEntity.SpellCards.Remove(spellCardEntity);
                        }
                    }

                    await dbContext.SaveChangesAsync();

                    return _mapper.Map<SpellDeckModel>(spellDeckEntity); ;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<bool> DeleteSpellDeck(SpellDeckModel spellDeck)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    SpellDeckEntity spellDeckEntity = _mapper.Map<SpellDeckEntity>(spellDeck);
                    if (dbContext.SpellDecks.Contains(spellDeckEntity))
                    {
                        dbContext.SpellDecks.Remove(spellDeckEntity);
                        await dbContext.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<IEnumerable<SpellDeckModel>> GetAllSpellDecks()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<SpellDeckEntity> spellDeckEntities = await
                    context.SpellDecks
                    .Include(sd => sd.Characters)
                    .Include(sd => sd.SpellCards)
                    .ToListAsync();

                return spellDeckEntities.Select(c => _mapper.Map<SpellDeckModel>(c));
            }
        }
    }
}
