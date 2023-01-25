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
    public class DatabaseSpellCardService : ISpellCardService
    {
        private readonly CardDesignerDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public DatabaseSpellCardService(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<SpellCardModel> CreateSpellCard(SpellCardModel spellCard)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                SpellCardEntity spellCardEntity = _mapper.Map<SpellCardEntity>(spellCard);

                SpellCardEntity createdSpellCardEntity = dbContext.SpellCards.Add(spellCardEntity).Entity;
                await dbContext.SaveChangesAsync();

                return _mapper.Map<SpellCardModel>(createdSpellCardEntity);
            }
        }

        public async Task<SpellCardModel> UpdateSpellCard(SpellCardModel spellCard)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                SpellCardEntity spellCardEntity = _mapper.Map<SpellCardEntity>(spellCard);

                SpellCardEntity createdSpellCardEntity = dbContext.SpellCards.Update(spellCardEntity).Entity;
                await dbContext.SaveChangesAsync();

                return _mapper.Map<SpellCardModel>(spellCardEntity);
            }
        }

        public async Task<bool> DeleteSpellCard(SpellCardModel spellCard)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    SpellCardEntity spellCardEntity = _mapper.Map<SpellCardEntity>(spellCard);
                    if (dbContext.SpellCards.Contains(spellCardEntity))
                    {
                        dbContext.SpellCards.Remove(spellCardEntity);
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

        public async Task<IEnumerable<SpellCardModel>> GetAllSpellCards()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<SpellCardEntity> characterEntities = await
                    context.SpellCards
                    .Include(sc => sc.SpellDecks)
                    .ToListAsync();

                return characterEntities.Select(c => _mapper.Map<SpellCardModel>(c));
            }
        }
    }
}
