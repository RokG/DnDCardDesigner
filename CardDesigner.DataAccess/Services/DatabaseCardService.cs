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
    public class DatabaseCardService : ICardService
    {
        private readonly CardDesignerDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public DatabaseCardService(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<ICard> CreateCard(ICard cardModel)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                switch (cardModel)
                {
                    case SpellCardModel spellCardModel:

                        SpellCardEntity spellCardEntity = _mapper.Map<SpellCardEntity>(spellCardModel);
                        SpellCardEntity createdCardEntity = dbContext.SpellCards.Add(spellCardEntity).Entity;

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<SpellCardModel>(createdCardEntity);
                    case ItemCardModel itemCardModel:

                        ItemCardEntity itemCardEntity = _mapper.Map<ItemCardEntity>(itemCardModel);
                        ItemCardEntity createdItemCardEntity = dbContext.ItemCards.Add(itemCardEntity).Entity;

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<ItemCardModel>(createdItemCardEntity);
                    default:
                        return null;
                }
            }
        }

        public async Task<ICard> UpdateCard(ICard cardModel)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                switch (cardModel)
                {
                    case SpellCardModel spellCardModel:
                        SpellCardEntity spellCardEntity = _mapper.Map<SpellCardEntity>(spellCardModel);
                        SpellCardEntity createdCardEntity = dbContext.SpellCards.Update(spellCardEntity).Entity;

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<SpellCardModel>(createdCardEntity);
                    case ItemCardModel itemCardModel:
                        ItemCardEntity itemCardEntity = _mapper.Map<ItemCardEntity>(itemCardModel);
                        ItemCardEntity createdItemCardEntity = dbContext.ItemCards.Update(itemCardEntity).Entity;

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<ItemCardModel>(createdItemCardEntity);
                    default:
                        return null;
                }
            }
        }

        public async Task<bool> DeleteCard(ICard cardModel)
        {

            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                switch (cardModel)
                {
                    case SpellCardModel spellCardModel:
                        SpellCardEntity spellCardEntity = _mapper.Map<SpellCardEntity>(spellCardModel);
                        if (dbContext.SpellCards.Contains(spellCardEntity))
                        {
                            dbContext.SpellCards.Remove(spellCardEntity);
                            await dbContext.SaveChangesAsync();
                            return true;
                        }
                        return false;

                    case ItemCardModel itemCardModel:
                        ItemCardEntity itemCardEntity = _mapper.Map<ItemCardEntity>(itemCardModel);
                        if (dbContext.ItemCards.Contains(itemCardEntity))
                        {
                            dbContext.ItemCards.Remove(itemCardEntity);
                            await dbContext.SaveChangesAsync();
                            return true;
                        }
                        return false;
                    default:
                        return false;
                }
            }
        }

        public async Task<IEnumerable<T>> GetAllCards<T>()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                if (typeof(T) == typeof(SpellCardModel))
                {
                    IEnumerable<SpellCardEntity> spellCardEntities = await
                        context.SpellCards
                        .Include(sc => sc.SpellDecks)
                        .ToListAsync();

                    return (IEnumerable<T>)spellCardEntities.Select(c => _mapper.Map<SpellCardModel>(c));
                }
                else if (typeof(T) == typeof(ItemCardModel))
                {
                    IEnumerable<ItemCardEntity> itemCardEntities = await
                        context.ItemCards
                        .Include(sc => sc.ItemDecks)
                        .ToListAsync();

                    return (IEnumerable<T>)itemCardEntities.Select(c => _mapper.Map<ItemCardModel>(c));
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
