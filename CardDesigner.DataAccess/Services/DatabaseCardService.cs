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
                    case CharacterCardModel characterCardModel:
                        CharacterCardEntity characterCardEntity = _mapper.Map<CharacterCardEntity>(characterCardModel);
                        CharacterCardEntity createdCharacterCardEntity = dbContext.CharacterCards.Add(characterCardEntity).Entity;

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<CharacterCardModel>(createdCharacterCardEntity);
                    case MinionCardModel minionCardModel:

                        MinionCardEntity minionCardEntity = _mapper.Map<MinionCardEntity>(minionCardModel);
                        MinionCardEntity createdMinionCardEntity = dbContext.MinionCards.Add(minionCardEntity).Entity;

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<MinionCardModel>(createdMinionCardEntity);
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
                    case CharacterCardModel characterCardModel:
                        CharacterCardEntity characterCardEntity = _mapper.Map<CharacterCardEntity>(characterCardModel);
                        CharacterCardEntity createdCharacterCardEntity = dbContext.CharacterCards.Update(characterCardEntity).Entity;

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<CharacterCardModel>(createdCharacterCardEntity);
                    case MinionCardModel minionCardModel:
                        MinionCardEntity minionCardEntity = _mapper.Map<MinionCardEntity>(minionCardModel);
                        MinionCardEntity createdMinionCardEntity = dbContext.MinionCards.Update(minionCardEntity).Entity;

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<MinionCardModel>(createdMinionCardEntity);
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
                    case CharacterCardModel characterCardModel:
                        CharacterCardEntity characterCardEntity = _mapper.Map<CharacterCardEntity>(characterCardModel);
                        if (dbContext.CharacterCards.Contains(characterCardEntity))
                        {
                            dbContext.CharacterCards.Remove(characterCardEntity);
                            await dbContext.SaveChangesAsync();
                            return true;
                        }
                        return false;
                    case MinionCardModel minionCardModel:
                        MinionCardEntity minionCardEntity = _mapper.Map<MinionCardEntity>(minionCardModel);
                        if (dbContext.MinionCards.Contains(minionCardEntity))
                        {
                            dbContext.MinionCards.Remove(minionCardEntity);
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
                else if (typeof(T) == typeof(CharacterCardModel))
                {
                    IEnumerable<CharacterCardEntity> characterCardEntities = await
                        context.CharacterCards
                        .Include(sc => sc.CharacterDecks)
                        .ToListAsync();

                    return (IEnumerable<T>)characterCardEntities.Select(c => _mapper.Map<CharacterCardModel>(c));
                }
                else if (typeof(T) == typeof(MinionCardModel))
                {
                    IEnumerable<MinionCardEntity> minionCardEntities = await
                        context.MinionCards
                        .Include(sc => sc.MinionDecks)
                        .Include(sc => sc.Minion)
                        .ToListAsync();

                    return (IEnumerable<T>)minionCardEntities.Select(c => _mapper.Map<MinionCardModel>(c));
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
